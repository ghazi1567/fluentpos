// --------------------------------------------------------------------------------------------------
// <copyright file="SaleCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Constants;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Services;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.IntegrationServices.Logistics;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.DTOs.Inventory;
using FluentPOS.Shared.DTOs.Sales.Enums;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands
{
    internal sealed class SplitOrderCommandHandler :
        IRequestHandler<SplitAndAssignOrderCommand, bool>
    {
        private readonly IStockService _stockService;
        private readonly ISalesDbContext _salesContext;
        private readonly IMapper _mapper;
        private readonly IShopifyOrderFulFillmentService _shopifyOrderFulFillmentService;
        private readonly IOrderLogger _orderLogger;

        public SplitOrderCommandHandler(
            ISalesDbContext salesContext,
            IStockService stockService,
            IMapper mapper,
            IShopifyOrderFulFillmentService shopifyOrderFulFillmentService,
            IOrderLogger orderLogger)
        {
            _salesContext = salesContext;
            _stockService = stockService;
            _mapper = mapper;
            _shopifyOrderFulFillmentService = shopifyOrderFulFillmentService;
            _orderLogger = orderLogger;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<bool> Handle(SplitAndAssignOrderCommand command, CancellationToken cancellationToken)
        {
            Dictionary<string, Guid> whFOMapping = new Dictionary<string, Guid>();
            var splitOrderDetail = command.SplitOrderResult;
            var order = await _salesContext.Orders
               .Include(x => x.FulfillmentOrders).ThenInclude(x => x.FulfillmentOrderLineItems)
               .SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return false;
            }

            if (splitOrderDetail.IsSingleStore)
            {
                var warehouseId = splitOrderDetail.WarehouseIds.FirstOrDefault();
                order.WarehouseId = warehouseId;

                foreach (var fulfillmentOrder in order.FulfillmentOrders)
                {
                    if (fulfillmentOrder.Id == splitOrderDetail.FulfillmentOrderId)
                    {
                        await UpdateFulfillmentOrderAsync(fulfillmentOrder, warehouseId, splitOrderDetail.WarehouseStocks);
                    }
                }
            }
            else
            {
                var warehoustList = await _salesContext.Warehouses.ToListAsync();
                List<SplitOrderFOLineItemDto> splittedLineItems = new List<SplitOrderFOLineItemDto>();
                foreach (var warehouseId in splitOrderDetail.WarehouseIds)
                {
                    _orderLogger.LogInfo(splitOrderDetail.InternalOrderId.Value, splitOrderDetail.FulfillmentOrderId.Value, $"{OrderLogsConstant.SplitOrder} {warehouseId}");
                    // prepare split order payload
                    var splitOrderPayloadDto = PrepareSplitOrderPayload(warehouseId, splitOrderDetail.SplitOrderDetails);
                    int remaining = splitOrderDetail.SplitOrderDetails.Count() - splittedLineItems.Count();
                    if (remaining != splitOrderPayloadDto.fulfillment_order.fulfillment_order_line_items.Count())
                    {
                        splittedLineItems.AddRange(splitOrderPayloadDto.fulfillment_order.fulfillment_order_line_items);
                        var result = await _shopifyOrderFulFillmentService.SplitFulfillment(splitOrderDetail.FOShopifyId.Value, splitOrderPayloadDto);
                        if (result != null && result.fulfillmentOrderSplit != null && result.fulfillmentOrderSplit.FulfillmentOrderSplits.Length > 0)
                        {
                            string newFOId = ParseFOId(result.fulfillmentOrderSplit.FulfillmentOrderSplits.FirstOrDefault()?.RemainingFulfillmentOrder?.Id);
                            whFOMapping[newFOId] = warehouseId;
                        }
                        else
                        {
                            _orderLogger.LogInfo(splitOrderDetail.InternalOrderId.Value, splitOrderDetail.FulfillmentOrderId.Value, $"{OrderLogsConstant.SplitOrderFailed} {warehouseId}");
                        }
                    }
                    else
                    {
                        whFOMapping[$"{splitOrderDetail.FOShopifyId.Value}"] = warehouseId;
                    }
                }

                var fulfillmentOrders = await _shopifyOrderFulFillmentService.GetFulFillOrderByOrderId(order.ShopifyId.Value);
                var fulfillmentOrdersDto = _mapper.Map<List<InternalFulfillmentOrderDto>>(fulfillmentOrders);
                var newFulfillmentOrders = _mapper.Map<List<InternalFulfillmentOrder>>(fulfillmentOrdersDto);

                // TODO: Check if order already splitted then don't delete all FO.
                _salesContext.FulfillmentOrders.RemoveRange(order.FulfillmentOrders);
                order.FulfillmentOrders.AddRange(newFulfillmentOrders);
            }

            _salesContext.Orders.Update(order);
            await _salesContext.SaveChangesAsync();

            if (!splitOrderDetail.IsSingleStore)
            {
                var updatedOrder = await _salesContext.Orders
                    .Include(x => x.ShippingAddress)
                    .Include(x => x.LineItems)
              .Include(x => x.FulfillmentOrders).ThenInclude(x => x.FulfillmentOrderLineItems)
              .SingleOrDefaultAsync(x => x.Id == command.Id);

                updatedOrder.UpdateFulfillmentorders();

                foreach (var fulfillmentOrder in order.FulfillmentOrders)
                {
                    var warehouseId = whFOMapping[$"{fulfillmentOrder.ShopifyId.Value}"];
                    if (warehouseId != default)
                    {
                        await UpdateFulfillmentOrderAsync(fulfillmentOrder, warehouseId, splitOrderDetail.WarehouseStocks);
                    }
                }
                _salesContext.Orders.Update(updatedOrder);
                await _salesContext.SaveChangesAsync();
            }

            return true;
        }

        private string ParseFOId(string gid)
        {
            if (string.IsNullOrWhiteSpace(gid)) return string.Empty;
            string[] parts = gid.Split('/');
            return parts.LastOrDefault();
        }

        private SplitOrderPayloadDto PrepareSplitOrderPayload(Guid warehouseId, List<SplitOrderDetailDto> SplitOrderDetails)
        {
            var lineItems = SplitOrderDetails.Where(x => x.WarehouseId == warehouseId);
            return new SplitOrderPayloadDto
            {
                fulfillment_order = new SplitOrderFOPayloadDto
                {
                    fulfillment_order_line_items = lineItems.Select(x => new SplitOrderFOLineItemDto
                    {
                        id = x.LineItemId,
                        quantity = x.FulfillableQuantity
                    }).ToList()
                }
            };
        }

        private async Task UpdateFulfillmentOrderAsync(InternalFulfillmentOrder fulfillmentOrder, Guid warehouseId, List<WarehouseStockStatsDto> WarehouseStocks)
        {
            fulfillmentOrder.WarehouseId = warehouseId;
            fulfillmentOrder.OrderStatus = OrderStatus.AssignToOutlet;
            _orderLogger.LogInfo(fulfillmentOrder.Id, fulfillmentOrder.InternalOrderId, $"{warehouseId} {OrderLogsConstant.AssignedToOutlet} ");
            foreach (var fulfillmentOrderLineItem in fulfillmentOrder.FulfillmentOrderLineItems)
            {
                var warehouses = WarehouseStocks.Where(x => x.warehouseId == warehouseId && x.inventoryItemId == fulfillmentOrderLineItem.InventoryItemId.Value).ToList();
                WarehouseStockStatsDto stock = null;

                // check if item is available in multiple rack in same warehouse.
                string racks = GetRacksForLineItem(warehouses, fulfillmentOrderLineItem.Quantity.Value);

                stock = warehouses.FirstOrDefault();
                if (stock != null)
                {
                    fulfillmentOrderLineItem.StockId = string.IsNullOrEmpty(racks) ? stock.Id : null;
                    fulfillmentOrderLineItem.WarehouseId = stock.warehouseId;
                    fulfillmentOrderLineItem.SKU = stock.SKU;
                    fulfillmentOrderLineItem.Rack = string.IsNullOrEmpty(racks) ? stock.Rack : racks;
                    fulfillmentOrderLineItem.ProductId = stock.productId;

                    var response = await _stockService.RecordTransaction(new StockTransactionDto
                    {
                        IgnoreRackCheck = true,
                        inventoryItemId = stock.inventoryItemId,
                        productId = stock.productId,
                        quantity = fulfillmentOrderLineItem.Quantity.Value,
                        Rack = stock.Rack,
                        type = Shared.DTOs.Sales.Enums.OrderType.Commited,
                        warehouseId = stock.warehouseId,
                        SKU = stock.SKU,
                        VariantId = stock.VariantId
                    });
                }
            }
        }

        private string GetRacksForLineItem(List<WarehouseStockStatsDto> warehouses, long quantity)
        {
            string racks = string.Empty;
            if (warehouses.Count() > 0)
            {
                bool canSingleRackFulfill = warehouses.Any(x => x.quantity >= quantity);
                if (!canSingleRackFulfill)
                {
                    racks = string.Join(",", warehouses.Select(x => x.Rack).Distinct().ToList());
                }
            }

            return racks;
        }
    }
}