// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterSaleCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.DTOs.Inventory;
using FluentPOS.Shared.DTOs.Sales.Enums;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using System;
using System.Linq;

namespace FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands
{
    public class SplitAndAssignOrderCommand : IRequest<bool>
    {
        public SplitOrderResult SplitOrderResult { get; set; }

        public long Id { get; set; }

        public long ShopifyId { get; set; }

        public OrderStatus Status { get; set; }

        public long WarehouseId { get; set; }

        public OrderType OrderType { get; set; }

        public long? FulfillmentOrderId { get; set; }

        public IGrouping<long, WarehouseStockStatsDto> Warehouse { get; set; }

        public SplitAndAssignOrderCommand(SplitOrderResult splitOrderResult)
        {
            SplitOrderResult = splitOrderResult;
            Id = splitOrderResult.InternalOrderId.Value;
        }
    }
}