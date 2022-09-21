using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Enums
{
    public enum SalaryCalculationFormula
    {
        /// <summary>
        /// Rate = Total salary divided by required hours
        /// Payable = Rate * earned hours
        /// Overtime rate is calculated per 30 days
        /// </summary>
        RequiredHours = 1,

        /// <summary>
        /// Rate = Total salary divided by 30
        /// Payable = Rate * earned days
        /// </summary>
        DividedBy30 = 2,

        /// <summary>
        /// Rate = Total salary divided by number of days in the month
        /// Payable = Rate * earned days
        /// Overtime rate is calculated per 30 days
        /// </summary>
        DividedByDaysInMonth = 3,
    }
}