using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Inventory.Core.Entities
{
    public class ImportRecord : BaseEntity
    {
        public string Location { get; set; }

        public string SKU { get; set; }

        public int Qty { get; set; }

        public string Warehouse { get; set; }

        public string Status { get; set; } = "Pending";

        public string Note { get; set; }

        public string Rack { get; set; } = "Open";

        public bool IgnoreRackCheck { get; set; }
    }
}