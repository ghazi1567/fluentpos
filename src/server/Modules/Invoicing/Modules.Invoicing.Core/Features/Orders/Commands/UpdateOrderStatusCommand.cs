// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterSaleCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Enums;
using MediatR;
using System;

namespace FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands
{
    public class UpdateOrderStatusCommand : IRequest<Result<string>>
    {
        public long OrderId { get; set; }

        public long FulfillmentId { get; set; }

        public OrderStatus Status { get; set; }

        public UpdateOrderStatusCommand(long orderId, long fulfillmentId, OrderStatus status)
        {
            OrderId = orderId;
            FulfillmentId = fulfillmentId;
            Status = status;
        }
    }
}