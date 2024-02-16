// --------------------------------------------------------------------------------------------------
// <copyright file="SaleCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Services;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands
{
    internal sealed class LogisticsCommandHandler :
        IRequestHandler<UpdateStatusToDeliveredCommand, Result<bool>>
    {
        private readonly IStockService _stockService;
        private readonly ISalesDbContext _salesContext;
        private readonly IMapper _mapper;
        private readonly IShopifyOrderFulFillmentService _shopifyOrderFulFillmentService;
        private readonly IOrderLogger _orderLogger;

        public LogisticsCommandHandler(
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
        public async Task<Result<bool>> Handle(UpdateStatusToDeliveredCommand command, CancellationToken cancellationToken)
        {
            var foIds = command.Orders.Select(x => x.FulfillmentOrderId).ToList();
            var orders = await _salesContext.FulfillmentOrders
               .Where(x => foIds.Contains(x.Id)).ToListAsync();
            if (orders == null && orders.Count == 0)
            {
                return await Result<bool>.ReturnErrorAsync(string.Format("Orders not found"));
            }

            foreach (var order in orders)
            {
                order.OrderStatus = Shared.DTOs.Sales.Enums.OrderStatus.Delivered;
                order.UpdatedAt = DateTimeOffset.Now;
            }

            _salesContext.FulfillmentOrders.UpdateRange(orders);
            await _salesContext.SaveChangesAsync();
            return await Result<bool>.SuccessAsync("Order status updated to delivered");
        }
    }
}