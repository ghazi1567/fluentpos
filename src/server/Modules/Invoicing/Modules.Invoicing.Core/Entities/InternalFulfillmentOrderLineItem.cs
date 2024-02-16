using FluentPOS.Shared.Core.Domain;
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

        public long? WarehouseId { get; set; }

        public decimal? Price { get; set; }

        public long? StockId { get; set; }

        public string SKU { get; set; }

        public string Rack { get; set; }

        public long? ProductId { get; set; }

        public long? InternalFulfillmentOrderId { get; set; }
    }
}