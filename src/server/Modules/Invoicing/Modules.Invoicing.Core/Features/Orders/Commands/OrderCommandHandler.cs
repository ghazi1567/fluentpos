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
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands
{
    internal sealed class OrderCommandHandler :
        IRequestHandler<RegisterOrderCommand, Result<Guid>>,
        IRequestHandler<CancelledOrderCommand, Result<string>>,
        IRequestHandler<FulFillOrderCommand, Result<string>>,
        IRequestHandler<ApproveOrderCommand, Result<string>>,
        IRequestHandler<MoveLocationCommand, Result<string>>
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

        public OrderCommandHandler(
            IStringLocalizer<OrderCommandHandler> localizer,
            ISalesDbContext salesContext,
            ICartService cartService,
            IProductService productService,
            IStockService stockService,
            IEntityReferenceService referenceService,
            IMapper mapper,
            IShopifyOrderService shopifyOrderService,
            IShopifyOrderFulFillmentService shopifyOrderFulFillmentService)
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
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterOrderCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {

            var isExist = await _salesContext.Orders.SingleOrDefaultAsync(x => x.ShopifyId == command.ShopifyId);
            if (isExist != null)
            {
                return await Result<Guid>.ReturnErrorAsync(string.Format(_localizer["Duplicate: Order already exists {0}"], command.ShopifyId));
            }

            var order = _mapper.Map<InternalOrder>(command);
            string referenceNumber = await _referenceService.TrackAsync(order.GetType().Name);
            order.SetReferenceNumber(referenceNumber);

            if (command.Customer != null)
            {
                //var customer = _mapper.Map<GetCustomerByIdResponse>(command.Customer);
                //order.AddCustomer(customer);
            }

            await _salesContext.Orders.AddAsync(order, cancellationToken);
            await _salesContext.SaveChangesAsync(cancellationToken);

            // await _cartService.RemoveCartAsync(command.CartId);
            // foreach (var product in order.Products)
            // {
            //     await _stockService.RecordTransaction(product.ProductId, product.Quantity, order.ReferenceNumber);
            // }

            return await Result<Guid>.SuccessAsync(order.Id, string.Format(_localizer["Order {0} Created"], order.ReferenceNumber));
        }

        public async Task<Result<string>> Handle(CancelledOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.SingleOrDefaultAsync(x => x.ShopifyId == command.ShopifyId);
            if (order == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
            }

            await _shopifyOrderService.CancelOrder(command.ShopifyId, command.Reason);
            order.Status = Shared.DTOs.Sales.Enums.OrderStatus.Cancelled;
            order.CancelledAt = DateTimeOffset.Now;
            order.CancelReason = command.Reason;
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

            var fulFillment = await _shopifyOrderFulFillmentService.CompleteFulFillOrderAsync(order.ShopifyId.Value,command.FulFillOrderId, trackingInfo);

            var orderFulfillment = _mapper.Map<OrderFulfillment>(fulFillment);

            orderFulfillment.InternalOrderId = order.Id;
            order.Status = Shared.DTOs.Sales.Enums.OrderStatus.ReadyToShip;
            _salesContext.Orders.Update(order);
            await _salesContext.OrderFulfillment.AddAsync(orderFulfillment);
            await _salesContext.SaveChangesAsync();
            return await Result<string>.SuccessAsync("Order Cancelled", string.Format(_localizer["Order {0} Cancelled"], order.ShopifyId));
        }

        public async Task<Result<string>> Handle(ApproveOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _salesContext.Orders.SingleOrDefaultAsync(x => x.Id == command.Id);
            if (order == null)
            {
                return await Result<string>.ReturnErrorAsync(string.Format(_localizer["Order not found. Shopify Id: {0}"], command.ShopifyId));
            }

            order.Status = Shared.DTOs.Sales.Enums.OrderStatus.Approved;
            order.ApprovedAt = DateTimeOffset.Now;
            order.ApprovedBy = "test"; // TODO: set logged in user id.
            _salesContext.Orders.Update(order);
            await _salesContext.SaveChangesAsync(cancellationToken);

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
    }
}