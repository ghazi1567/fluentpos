// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterSaleCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.DTOs.Inventory;
using FluentPOS.Shared.DTOs.Sales.Enums;
using MediatR;
using System;
using System.Linq;

namespace FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands
{
    public class CityCorrectionOrderCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public long ShopifyId { get; set; }

        public OrderStatus Status { get; set; }

        public long FulfillmentOrderId { get; set; }

        public CityCorrectionOrderCommand(long id, long fulfillmentOrderId)
        {
            Id = id;
            FulfillmentOrderId = fulfillmentOrderId;
        }
    }
}