using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Enums
{
    public enum JobType
    {
        MarkAbsent = 1,
        MarkOffDay = 2,
        FetchCheckIn = 3,
        FetchCheckOut = 4,
    }
}