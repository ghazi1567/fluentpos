using FluentPOS.Shared.DTOs.Dtos;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Accounting.Core.Dtos
{
    public class PayrollDto : BaseEntityDto
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

        public string EmployeeName { get; set; }

        public List<PayrollTransactionDto> Transactions { get; set; }

        public string BankAccountNo { get; set; }

        public string BankAccountTitle { get; set; }

        public string BankName { get; set; }

        public string BankBranchCode { get; set; }

        public int TotalOffDays { get; set; }

        public int TotalHoliDays { get; set; }
    }
}
