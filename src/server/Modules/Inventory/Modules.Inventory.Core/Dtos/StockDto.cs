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

        public decimal AvailableQuantity { get;  set; }

        public DateTime LastUpdatedOn { get;  set; }

        public Guid WarehouseId { get; set; }
    }
}