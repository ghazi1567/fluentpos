using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Sales.Enums
{
    public enum OrderStatus : byte
    {
        Pending = 1,
        AssignToOutlet = 2,
        Verifying = 3,
        Preparing = 4,
        ReadyToShip = 5,
        Shipped = 6,
        Cancelled = 7,
        Approved = 8,
        InProgress = 9,
        PendingApproval = 10,
    }
}