using FluentPOS.Shared.DTOs.Sales.Enums;
using System;

namespace FluentPOS.Shared.DTOs.Inventory
{
    public class WarehouseStockStatsDto
    {
        public Guid productId { get; set; }

        public long inventoryItemId { get; set; }

        public long quantity { get; set; }

        public Guid warehouseId { get; set; }

        public string Rack { get; set; }

        public string SKU { get; set; }

        public long? VariantId { get; set; }

        public int Distance { get; set; }

        public long? Latitude { get; set; }

        public long? Longitude { get; set; }

        public string Name { get; set; }
    }
}