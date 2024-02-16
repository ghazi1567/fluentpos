// --------------------------------------------------------------------------------------------------
// <copyright file="SaleCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Services;
using FluentPOS.Modules.Invoicing.Core.Services.Confirmation;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.IntegrationServices.Logistics;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Inventory;
using FluentPOS.Shared.DTOs.Sales.Enums;
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
    internal sealed class OrderCommandHandler :
        IRequestHandler<RegisterOrderCommand, Result<long>>,
        IRequestHandler<CancelledOrderCommand, Result<string>>,
        IRequestHandler<FulFillOrderCommand, Result<string>>,
        IRequestHandler<ApproveOrderCommand, Result<string>>,
        IRequestHandler<MoveLocationCommand, Result<string>>,
        IRequestHandler<AssignWarehouseToOrderCommand, bool>,
        IRequestHandler<ConfirmOrderCommand, Result<string>>,
        IRequestHandler<ReturnOrderCommand, Result<string>>,
        IRequestHandler<AcceptOrderCommand, Result<string>>,
        IRequestHandler<RejectOrderCommand, Result<string>>,
        IRequestHandler<ReQueueOrderCommand, Result<string>>,
        IRequestHandler<UpdateOrderStatusCommand, Result<string>>,
        IRequestHandler<CityCorrectionOrderCommand, bool>
    {
        private readonly IEntityReferenceService _referenceService;
        private readonly IStockService _stockService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ISalesDbContext _salesContext;
        private readonly IStringLocalizer<OrderCommandHandler> _localizer;
        private readonly IMapper _mapper;
        private readonly IShopifyOrderService _shopifyOrderService;
        private readonly IShopifyOrderFulFillmentService _shopifyOrderFulFillmentService;
        private readonly IWarehouseService _warehouseService;
        private readonly IShopifyOrderSyncJob _shopifyOrderSyncJob;
        private readonly IPostexService _postexService;
        private readonly IOrderLogger _orderLogger;
        private readonly IConfirmationService _confirmationService;
        public OrderCommandHandler(
            IStringLocalizer<OrderCommandHandler> localizer,
            ISalesDbContext salesContext,
            ICartService cartService,
            IProductService productService,
            IStockService stockService,
            IEntityReferenceService referenceService,
            IMapper mapper,
            IShopifyOrderService shopifyOrderService,
            IShopifyOrderFulFillmentService shopifyOrderFulFillmentService,
            IWarehouseService warehouseService,
            IShopifyOrderSyncJob shopifyOrderSyncJob,
            IPostexService postexService,
            IOrderLogger orderLogger,
            IConfirmationService confirmationService)
        {
            _localizer = localizer;
            _salesContext = salesContext;
            _cartService = cartService;
            _productService = productService;
            _stockService = stockService;
            _referenceService = referenceService;
            _mapper = mapper;
            _shopifyOrderService = shopifyOrderService;
            _shopifyOrderFulFillmentService = shopifyOrderFulFillmentService;
            _warehouseService = warehouseService;
            _shopifyOrderSyncJob = shopifyOrderSyncJob;
            _postexService = postexService;
            _orderLogger = orderLogger;
            _confirmationService = confirmationService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<long>> Handle(RegisterOrderCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {

            var isExist = await _salesContext.Orders.FirstOrDefaultAsync(x => x.ShopifyId == command.ShopifyId);
            if (isExist != null)
            {
                return await Result<long>.ReturnErrorAsync(string.Format(_localizer["Duplicate: Order already exists {0}"], command.ShopifyId));
            }

            var order = _mapper.Map<InternalOrder>(command);

            if (order.ShippingAddress != null && (!order.ShippingAddress.Latitude.HasValue || !order.ShippingAddress.Longitude.HasValue))
            {
                var geoLocation = await _warehouseService.GetLatLongFromAdrressAsync(order.ShippingAddress.Address1, order.ShippingAddress.Address2, order.ShippingAddress.City, order.ShippingAddress.Zip, order.ShippingAddress.Country);

                // var geoLocation = await _warehouseService.GetLatLongFromAdrressAsync(order.ShippingAddress.Address1, order.ShippingAddress.Address2, order.ShippingAddress.City, order.ShippingAddress.Country);

                if (geoLocation != null)
                {
                    // decimal lat;
                    // decimal.TryParse(geoLocation.Lat, out lat);
                    // decimal lon;
                    // decimal.TryParse(geoLocation.lng, out lon);
                    order.ShippingAddress.Latitude = geoLocation.Lat;
                    order.ShippingAddress.Longitude = geoLocation.lng;
                }
            }

            order.UpdateFulfillmentorders();

            await _salesContext.Orders.AddAsync(order, cancellationToken);
            await _salesContext.SaveChangesAsync(cancellationToken);

            return await Result<long>.SuccessAsync(default(long), string.Format(_localizer["Order {0} Created"], order.ReferenceNumber));
        }

        public async Task<Result<string>> Handle(CancelledOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.Include(x => x.FulfillmentOrders).ThenInclude(x => x.FulfillmentOrderLineItems).SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
            }

            foreach (var item in order.FulfillmentOrders)
            {
                if (item.Id == command.FulfillmentOrderId)
                {
                    if (item.OrderStatus == OrderStatus.Shipped)
                    {
                        return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Unable to cancel, Order already shippped."]));
                    }

                    if (item.OrderStatus == OrderStatus.AssignToOutlet ||
                        item.OrderStatus == OrderStatus.Preparing ||
                        item.OrderStatus == OrderStatus.ReadyToShip ||
                        item.OrderStatus == OrderStatus.Verifying)
                    {
                        foreach (var inventoryItem in item.FulfillmentOrderLineItems)
                        {
                            if (inventoryItem.ProductId.HasValue)
                            {
                                var response = await _stockService.RecordTransaction(new StockTransactionDto
                                {
                                    IgnoreRackCheck = true,
                                    inventoryItemId = inventoryItem.InventoryItemId.Value,
                                    quantity = inventoryItem.Quantity.Value,
                                    type = Shared.DTOs.Sales.Enums.OrderType.uncommitted,
                                    productId = inventoryItem.ProductId.Value,
                                    SKU = inventoryItem.SKU,
                                    Rack = inventoryItem.Rack,
                                    warehouseId = inventoryItem.WarehouseId.Value,
                                    VariantId = inventoryItem.VariantId
                                });
                            }
                        }
                    }


                    if (order.FulfillmentOrders.Count == 1)
                    {
                        await _shopifyOrderService.CancelOrder(order.ShopifyId.Value, command.Reason);
                        order.Status = Shared.DTOs.Sales.Enums.OrderStatus.Cancelled;
                        order.CancelledAt = DateTimeOffset.Now;
                        order.CancelReason = command.Reason;

                        item.OrderStatus = Shared.DTOs.Sales.Enums.OrderStatus.Cancelled;
                        item.CancelledAt = DateTimeOffset.Now;
                    }
                    else
                    {
                        await _shopifyOrderService.CancelFulfillmentOrder(item.ShopifyId.Value);
                        item.OrderStatus = Shared.DTOs.Sales.Enums.OrderStatus.Cancelled;
                        item.CancelledAt = DateTimeOffset.Now;
                        item.CancelReason = command.Reason;
                    }
                }
            }

            _salesContext.Orders.Update(order);
            await _salesContext.SaveChangesAsync(cancellationToken);
            return await Result<string>.SuccessAsync("Order Cancelled", string.Format(_localizer["Order {0} Cancelled"], order.ShopifyId));
        }

        public async Task<Result<string>> Handle(FulFillOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
            }

            var trackingInfo = new ShopifySharp.TrackingInfo
            {
                Company = "TCS",
                Number = "123456",
                Url = "tcs.com"
            };

            var fulFillment = await _shopifyOrderFulFillmentService.CompleteFulFillOrderAsync(order.ShopifyId.Value, command.FulFillOrderId, trackingInfo);

            var orderFulfillment = _mapper.Map<IntenalFulfillment>(fulFillment);

            orderFulfillment.InternalOrderId = order.Id;
            order.Status = Shared.DTOs.Sales.Enums.OrderStatus.ReadyToShip;
            _salesContext.Orders.Update(order);
            await _salesContext.OrderFulfillment.AddAsync(orderFulfillment);
            await _salesContext.SaveChangesAsync();
            return await Result<string>.SuccessAsync("Order Cancelled", string.Format(_localizer["Order {0} Cancelled"], order.ShopifyId));
        }

        public async Task<Result<string>> Handle(ApproveOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.Include(x => x.FulfillmentOrders).SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
            }

            foreach (var item in order.FulfillmentOrders)
            {
                if (item.Id == command.FulFillOrderId)
                {
                    item.OrderStatus = OrderStatus.Confirmed;
                    order.ApprovedAt = DateTimeOffset.Now;
                    order.ApprovedBy = "test"; // TODO: set logged in user id.
                    order.SetNote(command.Reason);
                }
            }
            _salesContext.Orders.Update(order);
            await _salesContext.SaveChangesAsync(cancellationToken);
            _shopifyOrderSyncJob.ProcessOrder();
            return await Result<string>.SuccessAsync("Order Approved", string.Format(_localizer["Order {0} Approved"], order.ShopifyId));
        }

        public async Task<Result<string>> Handle(MoveLocationCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
            }

            bool status = await _shopifyOrderFulFillmentService.ChangeLocationAsync(order.ShopifyId.Value, command.NewLocationId);

            return await Result<string>.SuccessAsync("Order Warehouse Location Changed", string.Format(_localizer["Order {0} Warehouse Location Changed"], order.ShopifyId));
        }

        public async Task<bool> Handle(AssignWarehouseToOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders
                .Include(x => x.FulfillmentOrders).ThenInclude(x => x.FulfillmentOrderLineItems)
                .SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return false;
            }

            if (order.OrderType == Shared.DTOs.Sales.Enums.OrderType.SingleOrder)
            {
                order.WarehouseId = command.WarehouseId;
            }

            foreach (var item in order.FulfillmentOrders)
            {
                if (item.Id == command.FulfillmentOrderId)
                {
                    item.WarehouseId = command.WarehouseId;
                    item.OrderStatus = command.Status;
                    if (command.Warehouse != null)
                    {
                        foreach (var fulfillmentOrderLineItem in item.FulfillmentOrderLineItems)
                        {
                            var stock = command.Warehouse.FirstOrDefault(x => x.inventoryItemId == fulfillmentOrderLineItem.InventoryItemId);
                            if (stock != null)
                            {
                                fulfillmentOrderLineItem.StockId = stock.Id;
                                fulfillmentOrderLineItem.WarehouseId = stock.warehouseId;
                                fulfillmentOrderLineItem.SKU = stock.SKU;
                                fulfillmentOrderLineItem.Rack = stock.Rack;
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
                }
            }


            _salesContext.Orders.Update(order);
            await _salesContext.SaveChangesAsync();
            return true;
        }

        public async Task<Result<string>> Handle(ConfirmOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders
                .Include(x => x.LineItems)
                .Include(x => x.FulfillmentOrders).ThenInclude(x => x.FulfillmentOrderLineItems)
                .SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
            }

            var fulfillmentOrder = order.FulfillmentOrders.FirstOrDefault(x => x.ShopifyId == command.FulfillmentOrderId);

            foreach (var item in order.LineItems)
            {
                var confirmedItem = command.LineItems.FirstOrDefault(x => x.Id == item.Id);
                if (confirmedItem != null)
                {
                    item.ConfirmedQty = confirmedItem.ConfirmedQty;
                    item.ConfirmedAt = DateTimeOffset.Now;
                    item.WarehouseId = order.WarehouseId;
                }
            }

            List<StockTransactionDto> fulfilledStockList = new List<StockTransactionDto>();
            foreach (var item in fulfillmentOrder.FulfillmentOrderLineItems)
            {
                var confirmedItem = command.LineItems.FirstOrDefault(x => x.VariantId == item.VariantId);
                if (confirmedItem != null)
                {
                    item.ConfirmedQty = confirmedItem.ConfirmedQty;
                    item.ConfirmedAt = DateTimeOffset.Now;
                    item.WarehouseId = order.WarehouseId;
                    fulfilledStockList.Add(new StockTransactionDto
                    {
                        IgnoreRackCheck = true,
                        inventoryItemId = item.InventoryItemId.Value,
                        productId = item.ProductId.Value,
                        quantity = item.Quantity.Value,
                        Rack = item.Rack,
                        SKU = item.SKU,
                        type = OrderType.FulFill,
                        VariantId = item.VariantId,
                        warehouseId = item.WarehouseId.Value
                    });
                }
            }

            var trackingInfo = new ShopifySharp.TrackingInfo
            {
                Company = fulfillmentOrder.TrackingCompany,
                Number = fulfillmentOrder.TrackingNumber,
                Url = fulfillmentOrder.TrackingUrl
            };

            var fulFillment = await _shopifyOrderFulFillmentService.CompleteFulFillOrderAsync(order.ShopifyId.Value, command.FulfillmentOrderId, trackingInfo);

            var orderFulfillment = _mapper.Map<IntenalFulfillment>(fulFillment);

            orderFulfillment.InternalOrderId = order.Id;
            fulfillmentOrder.OrderStatus = Shared.DTOs.Sales.Enums.OrderStatus.ReadyToShip;

            await _stockService.RecordTransaction(fulfilledStockList);
            _salesContext.Orders.Update(order);
            await _salesContext.OrderFulfillment.AddAsync(orderFulfillment);
            await _salesContext.SaveChangesAsync();

            return await Result<string>.SuccessAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
        }

        public async Task<Result<string>> Handle(AcceptOrderCommand command, CancellationToken cancellationToken)
        {
            var fulfillmentOrder = await _salesContext.FulfillmentOrders
                .Include(x => x.FulfillmentOrderDestination)
                .Include(x => x.FulfillmentOrderLineItems)
                .SingleOrDefaultAsync(x => x.InternalOrderId == command.Id && x.ShopifyId == command.FulfillmentOrderId);
            if (fulfillmentOrder == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
            }

            var postexResponse = await _postexService.GenerateCNAsync(new Shared.DTOs.Dtos.Logistics.PostexOrderModel
            {
                CityName = fulfillmentOrder.FulfillmentOrderDestination.City,
                CustomerName = $"{fulfillmentOrder.FulfillmentOrderDestination.FirstName} {fulfillmentOrder.FulfillmentOrderDestination.LastName}",
                CustomerPhone = fulfillmentOrder.FulfillmentOrderDestination.Phone,
                DeliveryAddress = $"{fulfillmentOrder.FulfillmentOrderDestination.Address1} {fulfillmentOrder.FulfillmentOrderDestination.City} {fulfillmentOrder.FulfillmentOrderDestination.Country}",
                InvoiceDivision = 1,
                InvoicePayment = fulfillmentOrder.TotalPrice.Value,
                Items = fulfillmentOrder.FulfillmentOrderLineItems.Count(),
                OrderDetail = "",
                OrderRefNumber = fulfillmentOrder.Name,
                OrderType = "Normal",
                PickupAddressCode = "001"
            });

            if (postexResponse.StatusCode == "200")
            {
                // TODO: generate CN number here.
                fulfillmentOrder.TrackingCompany = "PostEx";
                fulfillmentOrder.TrackingUrl = "PostEx";
                fulfillmentOrder.TrackingNumber = postexResponse.Dist.TrackingNumber;
                fulfillmentOrder.TrackingStatus = postexResponse.Dist.OrderStatus;
                fulfillmentOrder.OrderStatus = Shared.DTOs.Sales.Enums.OrderStatus.Preparing;
                _salesContext.FulfillmentOrders.Update(fulfillmentOrder);
                await _salesContext.SaveChangesAsync(cancellationToken);

                return await Result<string>.SuccessAsync("Order Approved", string.Format(_localizer["Order {0} Approved"], fulfillmentOrder.ShopifyId));
            }

            return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Unable to generate CN# for order {0}."], fulfillmentOrder.ShopifyId));
        }

        public async Task<Result<string>> Handle(RejectOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
            }

            order.Status = Shared.DTOs.Sales.Enums.OrderStatus.ReQueueAfterReject;
            _salesContext.Orders.Update(order);
            await _salesContext.SaveChangesAsync(cancellationToken);

            return await Result<string>.SuccessAsync("Order Approved", string.Format(_localizer["Order {0} Approved"], order.ShopifyId));
        }

        public async Task<Result<string>> Handle(ReQueueOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.Include(x => x.FulfillmentOrders).ThenInclude(x => x.FulfillmentOrderLineItems).SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
            }

            foreach (var item in order.FulfillmentOrders)
            {
                if (item.ShopifyId == command.FulfillmentOrderId)
                {
                    if (item.OrderStatus == OrderStatus.AssignToHeadOffice)
                    {
                        await _orderLogger.MarkAsIgnoreAsync(item.Id);
                    }

                    item.OrderStatus = Shared.DTOs.Sales.Enums.OrderStatus.ReQueueAfterReject;
                    foreach (var inventoryItem in item.FulfillmentOrderLineItems)
                    {
                        if (inventoryItem.ProductId.HasValue)
                        {
                            var response = await _stockService.RecordTransaction(new StockTransactionDto
                            {
                                IgnoreRackCheck = true,
                                inventoryItemId = inventoryItem.InventoryItemId.Value,
                                quantity = inventoryItem.Quantity.Value,
                                type = Shared.DTOs.Sales.Enums.OrderType.uncommitted,
                                productId = inventoryItem.ProductId.Value,
                                SKU = inventoryItem.SKU,
                                Rack = inventoryItem.Rack,
                                warehouseId = inventoryItem.WarehouseId.Value,
                                VariantId = inventoryItem.VariantId
                            });
                        }
                    }
                }
            }

            _salesContext.Orders.Update(order);
            await _salesContext.SaveChangesAsync(cancellationToken);

            _shopifyOrderSyncJob.ProcessOrder();
            return await Result<string>.SuccessAsync("Order Approved", string.Format(_localizer["Order {0} Approved"], order.ShopifyId));
        }

        public async Task<bool> Handle(CityCorrectionOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders
                .Include(x => x.FulfillmentOrders)
                .SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return false;
            }

            order.Status = OrderStatus.CityCorrection;
            foreach (var item in order.FulfillmentOrders)
            {
                if (item.Id == command.FulfillmentOrderId)
                {
                    item.OrderStatus = OrderStatus.CityCorrection;
                }
            }

            _salesContext.Orders.Update(order);
            await _salesContext.SaveChangesAsync();
            return true;
        }

        public async Task<Result<string>> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.Include(x => x.FulfillmentOrders).ThenInclude(x => x.FulfillmentOrderDestination).SingleOrDefaultAsync(x => x.Id == command.OrderId);
            if (order == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.OrderId));
            }

            foreach (var item in order.FulfillmentOrders)
            {
                if (item.Id == command.FulfillmentId)
                {
                    item.OrderStatus = command.Status;

                    if (command.Status == OrderStatus.WAConfirmation)
                    {
                        var result = await _confirmationService.WhatsAppConfirmation(item);

                        if (result == false)
                        {
                            item.OrderStatus = OrderStatus.AssignedToCSR;
                        }
                    }
                }
            }

            _salesContext.Orders.Update(order);
            await _salesContext.SaveChangesAsync(cancellationToken);

            return await Result<string>.SuccessAsync("Order Approved", string.Format(_localizer["Order {0} Approved"], order.ShopifyId));
        }

        public async Task<Result<string>> Handle(ReturnOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders
                .Include(x => x.LineItems)
                .Include(x => x.FulfillmentOrders).ThenInclude(x => x.FulfillmentOrderLineItems)
                .SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
            }

            var fulfillmentOrder = order.FulfillmentOrders.FirstOrDefault(x => x.ShopifyId == command.FulfillmentOrderId);

            foreach (var item in order.LineItems)
            {
                var confirmedItem = command.LineItems.FirstOrDefault(x => x.Id == item.Id);
                if (confirmedItem != null)
                {
                    item.ConfirmedQty = confirmedItem.ConfirmedQty;
                    item.ConfirmedAt = DateTimeOffset.Now;
                }
            }

            List<StockTransactionDto> fulfilledStockList = new List<StockTransactionDto>();
            foreach (var item in fulfillmentOrder.FulfillmentOrderLineItems)
            {
                var confirmedItem = command.LineItems.FirstOrDefault(x => x.VariantId == item.VariantId);
                if (confirmedItem != null)
                {
                    item.ConfirmedQty = confirmedItem.ConfirmedQty;
                    item.ConfirmedAt = DateTimeOffset.Now;
                    if (confirmedItem.Condition == "Saleable")
                    {
                        fulfilledStockList.Add(new StockTransactionDto
                        {
                            IgnoreRackCheck = true,
                            inventoryItemId = item.InventoryItemId.Value,
                            productId = item.ProductId.Value,
                            quantity = item.Quantity.Value,
                            Rack = item.Rack,
                            SKU = item.SKU,
                            type = OrderType.StockReturn,
                            VariantId = item.VariantId,
                            warehouseId = item.WarehouseId.Value
                        });
                    }
                }
            }

            fulfillmentOrder.OrderStatus = Shared.DTOs.Sales.Enums.OrderStatus.Returned;
            await _stockService.RecordTransaction(fulfilledStockList);

            _salesContext.Orders.Update(order);
            await _salesContext.SaveChangesAsync();


            return await Result<string>.SuccessAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
        }
    }
}