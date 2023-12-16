using FluentPOS.Shared.DTOs.Sales.Enums;
using System;

namespace FluentPOS.Shared.DTOs.Inventory
{
    public class StockTransactionDto
    {
        public long productId { get; set; }

        public long inventoryItemId { get; set; }

        public long quantity { get; set; }

        public OrderType type { get; set; }

        public long warehouseId { get; set; }

        public string Rack { get; set; }

        public bool IgnoreRackCheck { get; set; }

        public string SKU { get; set; }

        public long? VariantId { get; set; }

        public long? ImportRecordId { get; set; }
    }
}