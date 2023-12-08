using System;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class VarianceProductReportDto
    {
        public long ProductId { get; set; }

        public decimal AvailableQuantity { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public string Name { get; set; }

        public decimal DiscountFactor { get; set; }

        public string BarcodeSymbology { get; set; }

    }
}