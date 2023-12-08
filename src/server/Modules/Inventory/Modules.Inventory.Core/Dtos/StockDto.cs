using System;

namespace FluentPOS.Modules.Inventory.Core.Dtos
{
    public class StockDto
    {

        public long ProductId { get; set; }

        public long InventoryItemId { get; set; }

        public decimal AvailableQuantity { get; set; }

        public decimal Committed { get; set; }

        public decimal OnHand { get; set; }

        public string Rack { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public long WarehouseId { get; set; }

        public string Title { get; set; }

        public string SKU { get; set; }

        public string WarehouseName { get; set; }
    }
}