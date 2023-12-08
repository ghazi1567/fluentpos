using FluentPOS.Shared.DTOs.Sales.Enums;
using System;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public record GetSalesResponse(long Id, string ReferenceNumber, DateTime TimeStamp, long CustomerId, string CustomerName, string CustomerPhone, OrderType OrderType, OrderStatus Status, string POReferenceNo, string Note);
}