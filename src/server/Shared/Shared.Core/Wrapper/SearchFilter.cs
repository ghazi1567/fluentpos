using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Wrapper
{
    public abstract class SearchFilter
    {
        public string ProductId { get; set; }

        public string WarehouseId { get; set; }

        public string ReferenceNumber { get; set; }

        public string SearchString { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string SKU { get; set; }

        public string Barcode { get; set; }
    }
}