using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Utilities;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Inventory;
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

        public ShopifyOrderSyncJob(IHttpContextAccessor accessor, IMapper mapper, IMediator mediator, IJsonSerializer jsonSerializer,
            IWebhookEventService webhookEventService, IStoreService storeService, IStockService stockService)
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
                        order.Status = Shared.DTOs.Sales.Enums.OrderStatus.Pending;
                        var response = await _mediator.Send(order);
                        // Console.WriteLine(response.Messages);
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
                    var response = await SaveOrder(command);
                    if (response.Succeeded)
                    {
                        isFoundNewOrder = true;
                        await _webhookEventService.UpdateStatus(order.Id, "Completed", "Order Saved Successully", command.ShopifyId);
                    }
                    else
                    {
                        string msg = string.Join(", ", response.Messages);
                        await _webhookEventService.UpdateStatus(order.Id, msg.Contains("Duplicate") ? "Duplicate" : "Failed", msg, command.ShopifyId);
                    }
                }
                catch (Exception ex)
                {
                    await _webhookEventService.UpdateStatus(order.Id, "Failed", ex.Message, null);
                }
            }

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
            foreach (var order in pendingOrders)
            {
                try
                {
                    // check city is valid
                    bool isValidCity = await CheckValidAddressAsync(order.ShippingAddress.City);
                    if (!isValidCity)
                    {
                        bool result = await _mediator.Send(new UpdateOrderStatusCommand(order.Id.Value, Shared.DTOs.Sales.Enums.OrderStatus.CityCorrection));
                        continue;
                    }

                    // check inventory and assign warehouse.
                    var skuQty = order.LineItems.ToDictionary(x => x.SKU, x => x.Quantity.Value);
                    var warehouse = await _stockService.CheckInventory(skuQty);
                    if (warehouse != null)
                    {
                        bool result = await _mediator.Send(new UpdateOrderStatusCommand(order.Id.Value, warehouse.Key, Shared.DTOs.Sales.Enums.OrderStatus.AssignToOutlet));
                        await ReserveQtyAsync(warehouse, skuQty);
                        continue;
                    }
                    else
                    {
                        var result = await _mediator.Send(new GetDefaultWarehouseQuery());
                        if (result != null)
                        {
                            // default head office warehouse found.
                            await _mediator.Send(new UpdateOrderStatusCommand(order.Id.Value, result.Id.Value, Shared.DTOs.Sales.Enums.OrderStatus.AssignToHeadOffice));
                        }
                        else
                        {
                            // default head office warehouse not found.
                            await _mediator.Send(new UpdateOrderStatusCommand(order.Id.Value, Guid.Empty, Shared.DTOs.Sales.Enums.OrderStatus.AssignToHeadOffice));
                        }
                    }
                }
                catch (Exception ex)
                {
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

        #endregion
    }
}