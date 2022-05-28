using FluentPOS.Shared.DTOs.Sales.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public record GetSalesResponse(Guid Id, string ReferenceNumber, DateTime TimeStamp, Guid CustomerId, string CustomerName, string CustomerPhone, OrderType OrderType, OrderStatus Status, string POReferenceNo);
}