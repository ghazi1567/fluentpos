using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public record VarianceProductReport
    (
        long ProductId,
        decimal AvailableQuantity,
        DateTime LastUpdatedOn,
        string Name,
        decimal DiscountFactor,
        string BarcodeSymbology
    );
}