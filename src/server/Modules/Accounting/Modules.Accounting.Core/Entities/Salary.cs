using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Accounting.Core.Entities
{
    public class Salary : BaseEntity
    {
        public long EmployeeId { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal CurrentSalary { get; set; }

        public decimal Incentive { get; set; }

        public decimal Deduction { get; set; }

        public decimal PayableSalary { get; set; }

        public decimal PerDaySalary { get; set; }

        public decimal PerHourSalary { get; set; }

        public decimal TotalDaysInMonth { get; set; }

        public bool Active { get; set; }
    }
}