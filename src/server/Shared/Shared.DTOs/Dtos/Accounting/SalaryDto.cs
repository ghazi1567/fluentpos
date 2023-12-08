using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Dtos.Accounting
{
    public class SalaryDto : BaseEntityDto
    {
        public long EmployeeId { get; set; }

        public double BasicSalary { get; set; }

        public double Incentive { get; set; }

        public double Deduction { get; set; }

        public double PayableSalary { get; set; }

        public double PerDaySalary { get; set; }

        public double PerHourSalary { get; set; }

        public int TotalDaysInMonth { get; set; }
    }
}
