using FluentPOS.Shared.DTOs.Dtos;
using System;

namespace FluentPOS.Shared.DTOs.Inventory
{
    public class WarehouseStockStatsDto : BaseEntityDto
    {
        public long productId { get; set; }

        public long inventoryItemId { get; set; }

        public long quantity { get; set; }

        public long warehouseId { get; set; }

        public string Rack { get; set; }

        public string SKU { get; set; }

        public long? VariantId { get; set; }

        public decimal Distance { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string Name { get; set; }
    }
}