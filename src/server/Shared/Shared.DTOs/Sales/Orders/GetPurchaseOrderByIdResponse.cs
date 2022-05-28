using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public record GetPurchaseOrderByIdResponse
    (
     Guid Id,
     string ReferenceNumber,
     DateTime TimeStamp,
     decimal Total,
     bool IsApproved,
     DateTime? ApproveDate,
     Guid ApproveBy,
     string Note,
     ICollection<POProductResponse> Products,
     Guid WarehouseId
    );

}