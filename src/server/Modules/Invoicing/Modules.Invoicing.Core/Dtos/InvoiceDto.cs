using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.DTOs.Dtos;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class InvoiceDto: BaseEntityDto
    {
        public string CPRNumber { get; set; }

        public DateTime CPRDate { get; set; }

        public double ToTalCODAmount { get; set; }

        public double TotalReserveAmount { get; set; }

        public double TotalShippingCharges { get; set; }

        public double TotalNetAmount { get; set; }

        public double TotalTax { get; set; }

        public double TotalReceivable { get; set; }

        public long TotalDelivered { get; set; }

        public long TotalReturned { get; set; }

        public List<InvoiceDetailDto> InvoiceDetails { get; set; }
    }
}
