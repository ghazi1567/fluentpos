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
    public class AssignWarehouseToOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public long ShopifyId { get; set; }

        public OrderStatus Status { get; set; }

        public Guid WarehouseId { get; set; }

        public OrderType OrderType { get; set; }

        public Guid? FulfillmentOrderId { get; set; }

        public IGrouping<Guid, WarehouseStockStatsDto> Warehouse { get; set; }


        public AssignWarehouseToOrderCommand(Guid id, OrderStatus status)
        {
            Id = id;
            Status = status;
        }

        public AssignWarehouseToOrderCommand(Guid id, Guid warehouseId, OrderStatus status)
        {
            Id = id;
            WarehouseId = warehouseId;
            Status = status;
        }

        public AssignWarehouseToOrderCommand(Guid id, Guid warehouseId, OrderStatus status, Guid? fulfillmentOrderId)
        {
            Id = id;
            WarehouseId = warehouseId;
            Status = status;
            FulfillmentOrderId = fulfillmentOrderId;
        }

        public AssignWarehouseToOrderCommand(Guid id, OrderStatus status, Guid? fulfillmentOrderId, IGrouping<Guid, WarehouseStockStatsDto> warehouse)
        {
            Id = id;
            WarehouseId = warehouse.Key;
            Status = status;
            FulfillmentOrderId = fulfillmentOrderId;
            Warehouse = warehouse;
        }
    }
}