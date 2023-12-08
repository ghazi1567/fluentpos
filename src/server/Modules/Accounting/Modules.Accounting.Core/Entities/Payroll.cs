using System;
using System.Collections.Generic;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Enums;

namespace FluentPOS.Modules.Accounting.Core.Entities
{
    public class Payroll : BaseEntity
    {
        public long PayrollRequestId { get; set; }

        public long EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Month { get; set; }

        public PayPeriod PayPeriod { get; set; }

        public double EmployeeSalary { get; set; }

        public int RequiredDays { get; set; }

        public int EarnedDays { get; set; }

        public int TotalDays { get; set; }

        public int PresentDays { get; set; }

        public int AbsentDays { get; set; }

        public int leaves { get; set; }

        public int AllowedOffDays { get; set; }

        public PaymentMode PaymentMode { get; set; }

        public double TotalEarned { get; set; }

        public double TotalIncentive { get; set; }

        public double TotalDeduction { get; set; }

        public double TotalOvertime { get; set; }

        public double NetPay { get; set; }

        public List<PayrollTransaction> Transactions { get; set; }

        public string BankAccountNo { get; set; }

        public string BankAccountTitle { get; set; }

        public string BankName { get; set; }

        public string BankBranchCode { get; set; }

        public int TotalOffDays { get; set; }

        public int TotalHoliDays { get; set; }

        public PayslipStatus Status { get; set; }
    }
}