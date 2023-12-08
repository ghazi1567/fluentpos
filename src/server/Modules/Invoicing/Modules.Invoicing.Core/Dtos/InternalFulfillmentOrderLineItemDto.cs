using FluentPOS.Shared.DTOs.Dtos;
using System;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class InternalFulfillmentOrderLineItemDto : BaseEntityDto
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
    }
}
