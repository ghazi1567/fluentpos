// --------------------------------------------------------------------------------------------------
// <copyright file="Stock.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Inventory.Core.Entities
{
    public class Stock : BaseEntity
    {
        public Stock()
        {
        }

        public Stock(Guid productId)
        {
            ProductId = productId;
            LastUpdatedOn = DateTime.Now;
        }

        public Stock(Guid productId, Guid warehouseId)
        {
            ProductId = productId;
            LastUpdatedOn = DateTime.Now;
            WarehouseId = warehouseId;
        }

        public Stock(Guid productId, long inventoryItemId, Guid warehouseId)
        {
            ProductId = productId;
            InventoryItemId = inventoryItemId;
            LastUpdatedOn = DateTime.Now;
            WarehouseId = warehouseId;
        }

        public Guid ProductId { get; private set; }

        public long InventoryItemId { get; set; }

        public long AvailableQuantity { get; private set; }

        public long Committed { get; private set; }

        public long OnHand { get; private set; }

        public string Rack { get; set; }

        public DateTime LastUpdatedOn { get; private set; }

        public Guid WarehouseId { get; set; }

        public void IncreaseQuantity(long quantity)
        {
            AvailableQuantity += quantity;
            OnHand += quantity;
            LastUpdatedOn = DateTime.Now;
        }

        public void UpdateQuantity(long quantity)
        {
            AvailableQuantity = quantity - Committed;
            OnHand = quantity;
            LastUpdatedOn = DateTime.Now;
        }

        public void IncreaseCommittedQuantity(long quantity)
        {
            AvailableQuantity -= quantity;
            Committed += quantity;
            LastUpdatedOn = DateTime.Now;
        }

        public void ReduceCommittedQuantity(long quantity)
        {
            AvailableQuantity += quantity;
            Committed -= quantity;
            LastUpdatedOn = DateTime.Now;
        }

        public void FulFillCommittedQuantity(long quantity)
        {
            Committed -= quantity;
            OnHand -= quantity;
            LastUpdatedOn = DateTime.Now;
        }
    }
}