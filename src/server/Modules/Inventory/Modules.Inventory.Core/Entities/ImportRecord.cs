using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Inventory.Core.Entities
{
    public class ImportRecord : BaseEntity
    {
        public long LocationId { get; set; }

        public string SKU { get; set; }

        public long VariantId { get; set; }

        public int Qty { get; set; }

        public long WarehouseId { get; set; }

        public string Warehouse { get; set; }

        public string Status { get; set; } = "Pending";

        public string Note { get; set; }

        public string Rack { get; set; } = "Open";

        public bool IgnoreRackCheck { get; set; }

        public bool IsUpdatedOnShopify { get; set; }

        public long InventoryItemId { get; set; }

        public long ProductId { get; set; }
    }
}