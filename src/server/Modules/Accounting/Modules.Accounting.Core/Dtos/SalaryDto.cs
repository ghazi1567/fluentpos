using FluentPOS.Shared.DTOs.Dtos;
using System;

namespace FluentPOS.Modules.Accounting.Core.Dtos
{
    public class SalaryDto : BaseEntityDto
    {
        public Guid EmployeeId { get; set; }

        public double BasicSalary { get; set; }

        public double Incentive { get; set; }

        public double Deduction { get; set; }

        public double PayableSalary { get; set; }

        public double PerDaySalary { get; set; }

        public double PerHourSalary { get; set; }

        public int TotalDaysInMonth { get; set; }
    }
}
