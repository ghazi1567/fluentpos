﻿using FluentPOS.Shared.Core.Domain;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    [Table("FulfillmentLineItems")]
    public class InternalFulfillmentOrderLineItem : BaseEntity
    {
        public long? ShopId { get; set; }

        public long? FulfillmentOrderId { get; set; }

        public long? LineItemId { get; set; }

        public long? InventoryItemId { get; set; }

        public long? Quantity { get; set; }

        public long? FulfillableQuantity { get; set; }

        public long? VariantId { get; set; }

        public long? ConfirmedQty { get; set; }

        public DateTimeOffset? ConfirmedAt { get; set; }

        public Guid? WarehouseId { get; set; }

        public decimal? Price { get; set; }

        public Guid? StockId { get; set; }

        public string SKU { get; set; }

        public string Rack { get; set; }

        public Guid? ProductId { get; set; }
    }
}