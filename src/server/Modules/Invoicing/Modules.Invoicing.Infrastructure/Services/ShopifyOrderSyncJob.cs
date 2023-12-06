using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Constants;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Modules.Invoicing.Core.Services;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Utilities;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Inventory;
using FluentPOS.Shared.DTOs.Sales.Orders;
using FluentPOS.Shared.Infrastructure.Services;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopifySharp;
using ShopifySharp.Filters;
using ShopifySharp.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public class ShopifyOrderSyncJob : HangfireService, IShopifyOrderSyncJob
    {
        private const string WebhookOrderJobId = "ShopifyOrderJob1";

        private readonly IHttpContextAccessor _accessor;

        public readonly string StoreId;
        public readonly string _shopifyUrl;

        public readonly string _accessToken;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IWebhookEventService _webhookEventService;
        private readonly IStockService _stockService;
        private readonly IShopifyOrderFulFillmentService _shopifyOrderFulFillmentService;
        private readonly IWarehouseService _warehouseService;
        private readonly IOrderLogger _orderLogger;

        public ShopifyOrderSyncJob(
            IHttpContextAccessor accessor,
            IMapper mapper,
            IMediator mediator,
            IJsonSerializer jsonSerializer,
            IWebhookEventService webhookEventService,
            IStoreService storeService,
            IStockService stockService,
            IShopifyOrderFulFillmentService shopifyOrderFulFillmentService,
            IWarehouseService warehouseService,
            IOrderLogger orderLogger)
        {
            _accessor = accessor;
            _mapper = mapper;
            _mediator = mediator;
            _jsonSerializer = jsonSerializer;
            _webhookEventService = webhookEventService;
            _stockService = stockService;
            StoreId = _accessor.HttpContext?.Request?.Headers["store-id"];
            if (!string.IsNullOrEmpty(StoreId))
            {
                string shopifyCreds = EncryptionUtilities.DecryptString(StoreId);
                _shopifyUrl = shopifyCreds.Split("|")[0];
                _accessToken = shopifyCreds.Split("|")[1];
            }
            else
            {
                string shopifyCreds = storeService.StoreId();
                _shopifyUrl = shopifyCreds.Split("|")[0];
                _accessToken = shopifyCreds.Split("|")[1];
            }

            _shopifyOrderFulFillmentService = shopifyOrderFulFillmentService;
            _warehouseService = warehouseService;
            _orderLogger = orderLogger;
        }

        public async Task<bool> FetchAndSaveShopifyOrders(string shopifyUrl, string accessToken, ListFilter<ShopifySharp.Order> filter = null)
        {
            try
            {
                var service = new ShopifySharp.OrderService(shopifyUrl, accessToken);
                ListResult<ShopifySharp.Order> orders = null;

                if (filter != null)
                {
                    orders = await service.ListAsync(filter);
                }
                else
                {

                    orders = await service.ListAsync();
                }


                await SaveOrders(orders);

                if (orders.HasNextPage)
                {
                    await FetchAndSaveShopifyOrders(shopifyUrl, accessToken, orders.GetNextPageFilter());
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public string SyncShopifyOrders()
        {
            return Enqueue(() => FetchAndSaveShopifyOrders(_shopifyUrl, _accessToken, null));
        }

        public async Task SaveOrders(ListResult<ShopifySharp.Order> orders)
        {
            if (orders.Items.Count() > 0)
            {
                foreach (var shopifyProduct in orders.Items)
                {
                    try
                    {
                        var order = _mapper.Map<RegisterOrderCommand>(shopifyProduct);
                        var fulfillmentOrders = await _shopifyOrderFulFillmentService.GetFulFillOrderByOrderId(order.ShopifyId.Value);
                        order.FulfillmentOrders = _mapper.Map<List<InternalFulfillmentOrderDto>>(fulfillmentOrders);
                        order.Status = Shared.DTOs.Sales.Enums.OrderStatus.Pending;
                        var response = await _mediator.Send(order);
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        #region Order webhook job
        public string RunOrderWebhook()
        {
            SyncShopifyOrders();
            RemoveIfExists(WebhookOrderJobId);
            ScheduleRecurring(methodCall: () => FetchWebhookEvents(), recurringJobId: WebhookOrderJobId, cronExpression: Cron.MinuteInterval(5));
            return WebhookOrderJobId;
        }

        public async Task FetchWebhookEvents()
        {
            var pendingOrders = await _webhookEventService.FetchPendingOrders();
            var isFoundNewOrder = false;
            foreach (var order in pendingOrders)
            {
                try
                {
                    var shopifyOrder = JsonConvert.DeserializeObject<ShopifySharp.Order>(order.JsonBody);
                    var command = _mapper.Map<RegisterOrderCommand>(shopifyOrder);
                    var fulfillmentOrders = await _shopifyOrderFulFillmentService.GetFulFillOrderByOrderId(command.ShopifyId.Value);
                    command.FulfillmentOrders = _mapper.Map<List<InternalFulfillmentOrderDto>>(fulfillmentOrders);
                    _orderLogger.LogInfo(command.ShopifyId.Value, command.Id, OrderLogsConstant.WebhookStarted);
                    var response = await SaveOrder(command);
                    if (response.Succeeded)
                    {
                        isFoundNewOrder = true;
                        await _webhookEventService.UpdateStatus(order.Id, "Completed", "Order Saved Successully", command.ShopifyId);
                        _orderLogger.LogInfo(command.ShopifyId.Value, command.Id, OrderLogsConstant.NewOrderSaved);
                    }
                    else
                    {
                        string msg = string.Join(", ", response.Messages);
                        await _webhookEventService.UpdateStatus(order.Id, msg.Contains("Duplicate") ? "Duplicate" : "Failed", msg, command.ShopifyId);
                        _orderLogger.LogInfo(command.ShopifyId.Value, command.Id, OrderLogsConstant.NewOrderFailed);
                    }
                }
                catch (Exception ex)
                {
                    await _webhookEventService.UpdateStatus(order.Id, "Failed", ex.Message, null);
                    _orderLogger.LogInfo(order.Id, ex.Message);
                }
            }
            isFoundNewOrder = true;
            if (isFoundNewOrder)
            {
                ProcessOrder();
            }
        }

        public async Task<Result<Guid>> SaveOrder(RegisterOrderCommand registerOrderCommand)
        {
            return await _mediator.Send(registerOrderCommand);
        }
        #endregion

        #region Order process job
        public string ProcessOrder()
        {
            return Enqueue(() => FetchAndProcessOrders());
        }

        public async Task<bool> FetchAndProcessOrders()
        {
            var pendingOrders = await _mediator.Send(new GetOrderForProcessQuery());
            foreach (var fulfillmentOrder in pendingOrders)
            {
                try
                {
                    _orderLogger.LogInfo(fulfillmentOrder.Id.Value, fulfillmentOrder.InternalOrderId, OrderLogsConstant.ProcessOrderStart);
                    // check city is valid
                    bool isValidCity = await CheckValidAddressAsync(fulfillmentOrder.FulfillmentOrderDestination.City);
                    if (!isValidCity)
                    {
                        bool result = await _mediator.Send(new CityCorrectionOrderCommand(fulfillmentOrder.InternalOrderId, fulfillmentOrder.Id.Value));
                        if (result)
                        {
                            _orderLogger.LogInfo(fulfillmentOrder.Id.Value, fulfillmentOrder.InternalOrderId, OrderLogsConstant.CityCorrection);
                        }
                        else
                        {
                            _orderLogger.LogInfo(fulfillmentOrder.Id.Value, fulfillmentOrder.InternalOrderId, OrderLogsConstant.CityCorrectionFailed);
                        }

                        continue;
                    }

                    if (fulfillmentOrder.FulfillmentOrderLineItems.Count() > 0)
                    {
                        await InventoryAndWarehouseAssignmentAsync(fulfillmentOrder);
                    }
                }
                catch (Exception ex)
                {
                    _orderLogger.LogInfo(fulfillmentOrder.Id.Value, fulfillmentOrder.InternalOrderId, ex.Message);
                }
            }

            return true;
        }

        public async Task InventoryAndWarehouseAssignmentAsync(InternalFulfillmentOrderDto fulfillmentOrder)
        {
            _orderLogger.LogInfo(fulfillmentOrder.Id.Value, fulfillmentOrder.InternalOrderId, OrderLogsConstant.SearchingForAvailableQty);
            var skipLastWH = new List<Guid>();

            if (fulfillmentOrder.WarehouseId.HasValue)
            {
                skipLastWH.Add(fulfillmentOrder.WarehouseId.Value);
            }

            // check inventory and assign warehouse.
            // var warehouse = await _stockService.CheckInventory(variantQty, skipLastWH);

            // get stock list of inventory items
            var inventoryItemIds = fulfillmentOrder.FulfillmentOrderLineItems.Select(x => x.InventoryItemId.Value).ToList();
            var inventoryItemStockList = await _stockService.GetStockByVariantIds(inventoryItemIds);
            var warehouseIds = inventoryItemStockList.Select(x => x.warehouseId).ToList();
            var warehouseLlist = await _warehouseService.GetWarehouse(warehouseIds);

            // skip warehouse list
            if (warehouseLlist != null && warehouseLlist.Data.Count > 0)
            {
                warehouseLlist.Data = warehouseLlist.Data.Where(x => !skipLastWH.Contains(x.Id)).ToList();
                _orderLogger.LogInfo(fulfillmentOrder.Id.Value, fulfillmentOrder.InternalOrderId, OrderLogsConstant.LastWHSkiiped);
            }

            // calculate distance based on lat, long
            foreach (var itemStock in inventoryItemStockList)
            {
                itemStock.Latitude = fulfillmentOrder.FulfillmentOrderDestination.Latitude;
                itemStock.Longitude = fulfillmentOrder.FulfillmentOrderDestination.Longitude;
            }

            inventoryItemStockList = CalculateDistance(warehouseLlist.Data, inventoryItemStockList);
            _orderLogger.LogInfo(fulfillmentOrder.Id.Value, fulfillmentOrder.InternalOrderId, OrderLogsConstant.DistanceCalculated);

            // filter WH with valid qty
            var variantQty = fulfillmentOrder.FulfillmentOrderLineItems.ToDictionary(x => x.InventoryItemId.Value, x => x.Quantity.Value);
            var onlyValidStockList = FilterOnlyValidQtyWarehouse(inventoryItemStockList, variantQty);

            if (inventoryItemStockList.Count > 0)
            {
                // final picked warehouse base on near by available stock
                var splitOrderResult = FinalWarehousePick(fulfillmentOrder, fulfillmentOrder.FulfillmentOrderLineItems, inventoryItemStockList);
                splitOrderResult.WarehouseStocks = inventoryItemStockList;
                splitOrderResult.InternalOrderId = fulfillmentOrder.InternalOrderId;
                splitOrderResult.FulfillmentOrderId = fulfillmentOrder.Id;

                await AssignWarehouseAsync(splitOrderResult);
            }
            else
            {
                await AssignWarehouseAsync(fulfillmentOrder.InternalOrderId, Guid.Empty, fulfillmentOrder.Id);
            }
        }

        public async Task<bool> AssignWarehouseAsync(SplitOrderResult splitOrderResult)
        {
            bool result = await _mediator.Send(new SplitAndAssignOrderCommand(splitOrderResult));

            return result;
        }

        public async Task<bool> AssignWarehouseAsync(Guid internalOrderId, IGrouping<Guid, WarehouseStockStatsDto> warehouse, Guid? fulfillmentOrderId = null)
        {
            if (warehouse != null)
            {
                bool result = await _mediator.Send(new AssignWarehouseToOrderCommand(internalOrderId, Shared.DTOs.Sales.Enums.OrderStatus.AssignToOutlet, fulfillmentOrderId, warehouse));
            }
            else
            {
                var result = await _mediator.Send(new GetDefaultWarehouseQuery());
                if (result != null)
                {
                    // default head office warehouse found.
                    await _mediator.Send(new AssignWarehouseToOrderCommand(internalOrderId, result.Id.Value, Shared.DTOs.Sales.Enums.OrderStatus.AssignToHeadOffice, fulfillmentOrderId));
                }
                else
                {
                    // default head office warehouse not found.
                    await _mediator.Send(new AssignWarehouseToOrderCommand(internalOrderId, Guid.Empty, Shared.DTOs.Sales.Enums.OrderStatus.AssignToHeadOffice, fulfillmentOrderId));
                }
            }

            return true;
        }

        public async Task<bool> AssignWarehouseAsync(Guid internalOrderId, Guid? warehouseId, Guid? fulfillmentOrderId = null)
        {
            if (warehouseId.HasValue && warehouseId != Guid.Empty)
            {
                bool result = await _mediator.Send(new AssignWarehouseToOrderCommand(internalOrderId, warehouseId.Value, Shared.DTOs.Sales.Enums.OrderStatus.AssignToOutlet, fulfillmentOrderId));
            }
            else
            {
                var result = await _mediator.Send(new GetDefaultWarehouseQuery());
                if (result != null)
                {
                    // default head office warehouse found.
                    await _mediator.Send(new AssignWarehouseToOrderCommand(internalOrderId, result.Id.Value, Shared.DTOs.Sales.Enums.OrderStatus.AssignToHeadOffice, fulfillmentOrderId));
                }
                else
                {
                    // default head office warehouse not found.
                    await _mediator.Send(new AssignWarehouseToOrderCommand(internalOrderId, Guid.Empty, Shared.DTOs.Sales.Enums.OrderStatus.AssignToHeadOffice, fulfillmentOrderId));
                }
            }

            return true;
        }

        public async Task<bool> CheckValidAddressAsync(string cityName)
        {
            return await _mediator.Send(new CheckValidCityQuery(cityName));
        }


        public async Task<bool> ReserveQtyAsync(IGrouping<Guid, WarehouseStockStatsDto> warehouse, Dictionary<string, int> skuQty)
        {
            foreach (var inventoryItem in warehouse)
            {
                var response = await _stockService.RecordTransaction(new StockTransactionDto
                {
                    IgnoreRackCheck = true,
                    inventoryItemId = inventoryItem.inventoryItemId,
                    productId = inventoryItem.productId,
                    quantity = skuQty[inventoryItem.SKU],
                    Rack = inventoryItem.Rack,
                    type = Shared.DTOs.Sales.Enums.OrderType.Commited,
                    warehouseId = inventoryItem.warehouseId,
                    SKU = inventoryItem.SKU,
                    VariantId = inventoryItem.VariantId
                });
            }

            return true;
        }

        public async Task<bool> ReserveQtyAsync(IGrouping<Guid, WarehouseStockStatsDto> warehouse, Dictionary<long, long> skuQty)
        {
            foreach (var inventoryItem in warehouse)
            {
                var response = await _stockService.RecordTransaction(new StockTransactionDto
                {
                    IgnoreRackCheck = true,
                    inventoryItemId = inventoryItem.inventoryItemId,
                    productId = inventoryItem.productId,
                    quantity = skuQty[inventoryItem.inventoryItemId],
                    Rack = inventoryItem.Rack,
                    type = Shared.DTOs.Sales.Enums.OrderType.Commited,
                    warehouseId = inventoryItem.warehouseId,
                    SKU = inventoryItem.SKU,
                    VariantId = inventoryItem.VariantId
                });
            }

            return true;
        }

        public List<WarehouseStockStatsDto> CalculateDistance(List<GetWarehouseResponse> warehouses, List<WarehouseStockStatsDto> inventoryItemStockList)
        {
            foreach (var item in inventoryItemStockList)
            {
                var warehouse = warehouses.FirstOrDefault(x => x.Id == item.warehouseId);
                if (warehouse != null & item.Latitude.HasValue && item.Longitude.HasValue && !string.IsNullOrEmpty(warehouse.Latitude) && !string.IsNullOrEmpty(warehouse.Longitude))
                {
                    item.Distance = GetDistance(double.Parse(warehouse.Latitude), double.Parse(warehouse.Longitude), (double)item.Latitude, (double)item.Longitude);
                }
            }

            return inventoryItemStockList;
        }

        public decimal GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            double oneDegree = Math.PI / 180.0;
            double d1 = latitude * oneDegree;
            double num1 = longitude * oneDegree;
            double d2 = otherLatitude * oneDegree;
            double num2 = otherLongitude * oneDegree - num1;
            double d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            var meters = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
            return (decimal)(meters / 1000);
        }

        public List<IGrouping<Guid, WarehouseStockStatsDto>> FilterOnlyValidQtyWarehouse(List<WarehouseStockStatsDto> warehouseStockStats, Dictionary<long, long> skuQty)
        {
            List<WarehouseStockStatsDto> filterList = new List<WarehouseStockStatsDto>();

            foreach (var item in skuQty)
            {
                filterList.AddRange(warehouseStockStats.Where(x => x.inventoryItemId == item.Key && x.quantity >= item.Value).ToList());
            }

            return filterList.OrderBy(x => x.Distance).GroupBy(x => x.warehouseId).ToList();
        }

        public IGrouping<Guid, WarehouseStockStatsDto> FinalWarehousePick(List<IGrouping<Guid, WarehouseStockStatsDto>> ValidQtyStockStats, int skuCount)
        {
            var warehouseWithAllProducts = ValidQtyStockStats.Where(x => x.Count() >= skuCount).ToList();
            return warehouseWithAllProducts.FirstOrDefault();
        }

        public void SplitWarehouse(List<WarehouseStockStatsDto> warehouseStockStats, Dictionary<long, long> skuQty)
        {

        }

        public SplitOrderResult FinalWarehousePick(InternalFulfillmentOrderDto fulfillmentOrder, IEnumerable<InternalFulfillmentOrderLineItemDto> orderedProducts, List<WarehouseStockStatsDto> warehouses)
        {
            List<SplitOrderDetailDto> splitOrders = new List<SplitOrderDetailDto>();
            SplitOrderResult splitOrderResult = new SplitOrderResult();
            var productIds = orderedProducts.Select(p => p.InventoryItemId).ToList();
            var variantQty = orderedProducts.ToDictionary(x => x.InventoryItemId, x => x.Quantity);
            var lineItemIds = orderedProducts.ToDictionary(x => x.InventoryItemId, x => x.ShopifyId.Value);
            splitOrderResult.FOShopifyId = fulfillmentOrder.ShopifyId;
            splitOrderResult.AssignedLocationId = fulfillmentOrder.AssignedLocationId;

            // filter out warehouse with available qty
            var warehousesWithAvailability = warehouses
                .Where(w => w.quantity > 0 && productIds.Contains(w.inventoryItemId))
                .GroupBy(w => new { w.warehouseId, w.inventoryItemId })

                .Select(g => new SplitOrderDetailDto
                {
                    WarehouseId = g.Key.warehouseId,
                    InventoryItemId = g.Key.inventoryItemId,
                    AvailableQuantity = g.Sum(w => w.quantity),
                    CanFulFill = g.Sum(w => w.quantity) >= variantQty[g.Key.inventoryItemId],
                    Distance = g.First().Distance,
                    FulfillableQuantity = variantQty[g.Key.inventoryItemId].Value,
                    RequiredQuantity = variantQty[g.Key.inventoryItemId].Value,

                })
                .OrderByDescending(w => w.AvailableQuantity)
                .ToList();

            // group by warehouse with available qty
            var warehouseFulfillableQty = warehousesWithAvailability
              .Where(w => w.CanFulFill) // Consider only warehouses that can fulfill
              .GroupBy(w => w.WarehouseId) // Group by WarehouseId
              .Select(g => new SplitOrderDetailDto
              {
                  WarehouseId = g.Key,
                  CanFulfillCount = g.Select(w => w.InventoryItemId).Distinct().Count(),
                  FulfillableIds = g.Select(x => x.InventoryItemId).ToList(),
                  Distance = g.First().Distance,
              });

            // check if same store can fullfill all products.
            var canSingleStoreFulfill = warehouseFulfillableQty
                .OrderBy(x => x.Distance)
                .FirstOrDefault(x => x.CanFulfillCount == orderedProducts.Count());

            if (canSingleStoreFulfill != null)
            {
                _orderLogger.LogInfo(fulfillmentOrder.Id.Value, fulfillmentOrder.InternalOrderId, $"{OrderLogsConstant.SignleStoreFound} {canSingleStoreFulfill.WarehouseId}");

                splitOrders = canSingleStoreFulfill.FulfillableIds.Select(fid =>
                {
                    var warehouseAvailability = warehousesWithAvailability.FirstOrDefault(x => x.WarehouseId == canSingleStoreFulfill.WarehouseId && x.InventoryItemId == fid);
                    return new SplitOrderDetailDto
                    {
                        InventoryItemId = fid,
                        WarehouseId = canSingleStoreFulfill.WarehouseId,
                        AvailableQuantity = warehouseAvailability?.AvailableQuantity ?? 0,
                        Distance = warehouseAvailability?.Distance ?? 0,
                        FulfillableQuantity = warehouseAvailability?.FulfillableQuantity ?? 0,
                        CanFulFill = warehouseAvailability?.CanFulFill ?? false,
                        CanFulfillCount = warehouseAvailability?.CanFulfillCount ?? 0,
                        FulfillableIds = warehouseAvailability?.FulfillableIds,
                        RequiredQuantity = warehouseAvailability?.RequiredQuantity ?? 0
                    };
                }).ToList();

                splitOrderResult.IsSingleStore = true;
                splitOrderResult.SplitCount = 1;
                splitOrderResult.WarehouseIds = splitOrders.Select(x => x.WarehouseId.Value).Distinct().ToList();
                splitOrderResult.SplitOrderDetails = splitOrders;
                return splitOrderResult;
            }


            // if not fulfilled by one store.
            splitOrderResult.IsSingleStore = false;
            _orderLogger.LogInfo(fulfillmentOrder.Id.Value, fulfillmentOrder.InternalOrderId, OrderLogsConstant.CheckingForMultipleStore);
            foreach (var store in warehouseFulfillableQty.OrderByDescending(x => x.CanFulfillCount).ThenBy(x => x.Distance))
            {
                var remainingIds = store.FulfillableIds.Where(x => !splitOrders.Any(z => z.InventoryItemId == x)).ToList();
                if (remainingIds.Count > 0)
                {
                    splitOrders.AddRange(remainingIds.Select(fid =>
                    {
                        var warehouseAvailability = warehousesWithAvailability.FirstOrDefault(x => x.WarehouseId == store.WarehouseId && x.InventoryItemId == fid);
                        return new SplitOrderDetailDto
                        {
                            InventoryItemId = fid,
                            WarehouseId = store.WarehouseId,
                            AvailableQuantity = warehouseAvailability?.AvailableQuantity ?? 0,
                            Distance = warehouseAvailability?.Distance ?? 0,
                            FulfillableQuantity = warehouseAvailability?.FulfillableQuantity ?? 0,
                            CanFulFill = warehouseAvailability?.CanFulFill ?? false,
                            CanFulfillCount = warehouseAvailability?.CanFulfillCount ?? 0,
                            FulfillableIds = warehouseAvailability?.FulfillableIds,
                            RequiredQuantity = warehouseAvailability?.RequiredQuantity ?? 0,
                            LineItemId = lineItemIds[fid],
                        };
                    }).ToList());
                }
            }

            _orderLogger.LogInfo(fulfillmentOrder.Id.Value, fulfillmentOrder.InternalOrderId, OrderLogsConstant.CheckingForSingleProductMultipleStore);

            // check if one product can be fulfill by multple stores.
            var notFullfillableProducts = orderedProducts.Where(x => !splitOrders.Any(z => z.InventoryItemId == x.InventoryItemId)).ToList();
            var multipleWarehouseFulfillableQty = warehousesWithAvailability
              .Where(w => notFullfillableProducts.Any(z => z.InventoryItemId == w.InventoryItemId)) // Consider only warehouses that can fulfill
              .GroupBy(w => w.InventoryItemId) // Group by InventoryItemId
              .Select(g => new SplitOrderDetailDto
              {
                  InventoryItemId = g.Key,
                  AvailableQuantity = g.Sum(w => w.AvailableQuantity),
                  CanFulFill = g.Sum(w => w.RequiredQuantity) >= variantQty[g.Key],
                  Distance = g.First().Distance,
              });

            if (multipleWarehouseFulfillableQty.Any(x => x.CanFulFill))
            {
                var remainQty = notFullfillableProducts.ToDictionary(x => x.InventoryItemId.Value, x => x.Quantity.Value);

                foreach (var store in warehousesWithAvailability.Where(x => multipleWarehouseFulfillableQty.Any(z => z.InventoryItemId == x.InventoryItemId)).OrderByDescending(x => x.Distance))
                {
                    long qtyToFullfill = remainQty[store.InventoryItemId];
                    long canBeFullfilled = Math.Min(qtyToFullfill, store.AvailableQuantity);


                    splitOrders.Add(new SplitOrderDetailDto
                    {
                        InventoryItemId = store.InventoryItemId,
                        WarehouseId = store.WarehouseId,
                        AvailableQuantity = store.AvailableQuantity,
                        Distance = store.Distance,
                        FulfillableQuantity = canBeFullfilled,
                        CanFulFill = true,
                        CanFulfillCount = 0,
                        RequiredQuantity = store.RequiredQuantity,
                        LineItemId = lineItemIds[store.InventoryItemId]
                    });
                    remainQty[store.InventoryItemId] = qtyToFullfill - canBeFullfilled;
                }
            }

            splitOrderResult.WarehouseIds = splitOrders.Select(x => x.WarehouseId.Value).Distinct().ToList();
            splitOrderResult.SplitCount = splitOrderResult.WarehouseIds.Distinct().Count();

            // check if any product can not be fulfilled.
            notFullfillableProducts = orderedProducts.Where(x => !splitOrders.Any(z => z.InventoryItemId == x.InventoryItemId)).ToList();
            if (notFullfillableProducts.Count > 0)
            {
                splitOrderResult.SplitCount++;
                foreach (var item in notFullfillableProducts)
                {
                    splitOrders.Add(new SplitOrderDetailDto
                    {
                        InventoryItemId = item.InventoryItemId.Value,
                        WarehouseId = null,
                        AvailableQuantity = 0,
                        Distance = 0,
                        FulfillableQuantity = 0,
                        CanFulFill = false,
                        CanFulfillCount = 0,
                        RequiredQuantity = item.Quantity.Value,
                        LineItemId = lineItemIds[item.InventoryItemId.Value]
                    });
                }
            }

            splitOrderResult.SplitOrderDetails = splitOrders;
            return splitOrderResult;
        }
        #endregion
    }
}