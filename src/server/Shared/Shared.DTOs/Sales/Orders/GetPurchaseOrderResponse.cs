using System;
using FluentPOS.Shared.DTOs.Sales.Enums;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public record GetPurchaseOrderResponse(
        long Id,
        string ReferenceNumber,
        DateTime TimeStamp,
        decimal Total,
        bool IsApproved,
        DateTime? ApproveDate,
        long ApproveBy,
        string Note,
        OrderStatus Status
        );
}