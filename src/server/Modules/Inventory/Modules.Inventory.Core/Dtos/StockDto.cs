using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Core.Dtos
{
    public class StockDto
    {
        public Guid ProductId { get; private set; }

        public decimal AvailableQuantity { get; private set; }

        public DateTime LastUpdatedOn { get; private set; }

        public Guid WarehouseId { get; set; }
    }
}