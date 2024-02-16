using FluentPOS.Shared.Core.Domain;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public string CPRNumber { get; set; }

        public DateTime CPRDate { get; set; }

        public decimal ToTalCODAmount { get; set; }

        public decimal TotalReserveAmount { get; set; }

        public decimal TotalShippingCharges { get; set; }

        public decimal TotalNetAmount { get; set; }

        public decimal TotalTax { get; set; }

        public decimal TotalReceivable { get; set; }

        public long TotalDelivered { get; set; }

        public long TotalReturned { get; set; }

        public bool IsClosed { get; set; }

        public string Comments { get; set; }

        public bool IsValid { get; set; }

        public int InvalidCount { get; set; }

        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
