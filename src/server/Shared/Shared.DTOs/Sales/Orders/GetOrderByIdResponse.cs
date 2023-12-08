using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
       public record GetOrderByIdResponse
       (
              long Id,
              string ReferenceNumber,
              DateTime TimeStamp,
              long CustomerId,
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
              long WarehouseId
       );

    public record GetStockInByIdResponse
      (
             long Id,
             string ReferenceNumber,
             DateTime TimeStamp,
             long CustomerId,
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
             long WarehouseId
      );

}