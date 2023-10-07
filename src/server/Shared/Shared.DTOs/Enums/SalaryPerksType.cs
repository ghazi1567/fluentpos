using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Enums
{
    public enum SalaryPerksType
    {
        Increment = 1,
        Decrement = 2,
        Incentives = 3,
        Deductions = 4,
    }

    public enum GlobalSalaryPerksType
    {
        None = 0,
        EOBI = 1,
        FullMonthPresent = 2,
    }
}
