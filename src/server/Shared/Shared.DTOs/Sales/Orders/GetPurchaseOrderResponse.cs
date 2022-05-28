using System;
using FluentPOS.Shared.DTOs.Sales.Enums;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public record GetPurchaseOrderResponse(
        Guid Id,
        string ReferenceNumber,
        DateTime TimeStamp,
        decimal Total,
        bool IsApproved,
        DateTime? ApproveDate,
        Guid ApproveBy,
        string Note,
        OrderStatus Status
        );
}