using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
       public record GetOrderByIdResponse
       (
              Guid Id,
              string ReferenceNumber,
              DateTime TimeStamp,
              Guid CustomerId,
              string CustomerName,
              string CustomerPhone,
              string CustomerEmail,
              decimal SubTotal,
              decimal Tax,
              decimal Discount,
              decimal Total,
              bool IsPaid,
              string Note,
              ICollection<ProductResponse> Products,
              Guid WarehouseId
       );

    public record GetStockInByIdResponse
      (
             Guid Id,
             string ReferenceNumber,
             DateTime TimeStamp,
             Guid CustomerId,
             string CustomerName,
             string CustomerPhone,
             string CustomerEmail,
             decimal SubTotal,
             decimal Tax,
             decimal Discount,
             decimal Total,
             bool IsPaid,
             string Note,
             string POReferenceNo,
             ICollection<StockInProductResponse> Products,
             Guid WarehouseId
      );

}