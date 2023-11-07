using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Core.Dtos
{
    public class StockDto
    {

        public Guid ProductId { get;  set; }

        public long InventoryItemId { get; set; }

        public decimal AvailableQuantity { get;  set; }

        public decimal Committed { get;  set; }

        public decimal OnHand { get;  set; }

        public string Rack { get; set; }

        public DateTime LastUpdatedOn { get;  set; }

        public Guid WarehouseId { get; set; }
    }
}