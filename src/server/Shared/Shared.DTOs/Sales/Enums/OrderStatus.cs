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
        InProgress = 2,
        PendingApproval = 3,
        Approved = 4,
        Rejected = 5
    }
}