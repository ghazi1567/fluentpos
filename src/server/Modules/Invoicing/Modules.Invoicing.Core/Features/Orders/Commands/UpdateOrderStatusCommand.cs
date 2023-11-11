// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterSaleCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.DTOs.Sales.Enums;
using MediatR;
using System;

namespace FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands
{
    public class UpdateOrderStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public long ShopifyId { get; set; }

        public OrderStatus Status { get; set; }

        public Guid WarehouseId { get; set; }

        public UpdateOrderStatusCommand(Guid id, OrderStatus status)
        {
            Id = id;
            Status = status;
        }

        public UpdateOrderStatusCommand(Guid id, Guid warehouseId, OrderStatus status)
        {
            Id = id;
            WarehouseId = warehouseId;
            Status = status;
        }
    }
}