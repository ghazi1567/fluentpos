using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Enums
{
    public enum RequestStatus
    {
        Pending = 0,
        Rejected = 1,
        Approved = 2,
        InProgress = 3
    }
}