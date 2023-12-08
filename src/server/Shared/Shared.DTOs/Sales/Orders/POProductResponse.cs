using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public record POProductResponse
    (
        long OrderId,
        long ProductId,
        int Quantity,
        string Category,
        string Brand,
        decimal Price,
        decimal Tax,
        decimal Discount,
        decimal Total,
        long PurchaseOrderId
);
}