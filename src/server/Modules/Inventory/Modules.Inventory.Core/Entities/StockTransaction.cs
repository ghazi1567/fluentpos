// --------------------------------------------------------------------------------------------------
// <copyright file="StockTransaction.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Sales.Enums;
using System;

namespace FluentPOS.Modules.Inventory.Core.Entities
{
    public class StockTransaction : BaseEntity
    {
        public StockTransaction(long productId, long inventoryItemId, long quantity, OrderType type, long warehouseId)
        {
            ProductId = productId;
            Quantity = quantity;
            Type = type;
            InventoryItemId = inventoryItemId;
            Timestamp = DateTime.Now;
            WarehouseId = warehouseId;
        }

        public StockTransaction(long productId, long quantity, OrderType type, string referenceNumber)
        {
            ProductId = productId;
            Quantity = quantity;
            Type = type;
            ReferenceNumber = referenceNumber;
            Timestamp = DateTime.Now;
        }

        public StockTransaction(long productId, long quantity, OrderType type, string referenceNumber, decimal discountFactor, decimal purchasePrice, DateTime factorDate)
        {
            ProductId = productId;
            Quantity = quantity;
            Type = type;
            ReferenceNumber = referenceNumber;
            Timestamp = DateTime.Now;
            DiscountFactor = discountFactor;
            PurchasePrice = purchasePrice;
            FactorDate = factorDate;
        }

        public StockTransaction(long productId, long quantity, OrderType type, string referenceNumber, decimal discountFactor, decimal purchasePrice, DateTime factorDate, DateTime timestamp)
        {
            ProductId = productId;
            Quantity = quantity;
            Type = type;
            ReferenceNumber = referenceNumber;
            Timestamp = timestamp;
            DiscountFactor = discountFactor;
            PurchasePrice = purchasePrice;
            FactorDate = factorDate;
        }

        public long ProductId { get; private set; }

        public long InventoryItemId { get; set; }

        public DateTime Timestamp { get; private set; }

        public long Quantity { get; set; }

        public OrderType Type { get; private set; }

        public string ReferenceNumber { get; private set; }

        public decimal DiscountFactor { get; set; }

        public decimal PurchasePrice { get; set; }

        public DateTime FactorDate { get; set; }

        public long WarehouseId { get; set; }
    }
}