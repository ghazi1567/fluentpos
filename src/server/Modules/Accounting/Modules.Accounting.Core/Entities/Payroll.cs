using System;
using System.Collections.Generic;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Accounting.Core.Entities
{
    public class Payroll : BaseEntity
    {
        public Guid PayrollRequestId { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double EmployeeSalary { get; set; }

        public int RequiredDays { get; set; }

        public int EarnedDays { get; set; }

        public int TotalDays { get; set; }

        public int PresentDays { get; set; }

        public int AbsentDays { get; set; }

        public int leaves { get; set; }

        public int AllowedOffDays { get; set; }

        public int PaymentMode { get; set; }

        public double TotalEarned { get; set; }

        public double TotalIncentive { get; set; }

        public double TotalDeduction { get; set; }

        public double TotalOvertime { get; set; }

        public double NetPay { get; set; }

        public List<PayrollTransaction> Transactions { get; set; }
    }
}
