using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Enums
{
    public enum AttendanceStatus
    {
        None = 0,
        Present = 1,
        Absent = 2,
        Leave = 3,
        Holiday = 4,
        Off = 5
    }
}
