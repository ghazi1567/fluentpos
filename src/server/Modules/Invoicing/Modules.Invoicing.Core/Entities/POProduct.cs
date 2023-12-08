using FluentPOS.Shared.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    public class POProduct : BaseEntity
    {
        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }

        public decimal Price { get; set; }

        public decimal Tax { get; set; }

        public decimal Discount { get; set; }

        public decimal Total { get; set; }
        public long PurchaseOrderId { get; set; }
    }
}
