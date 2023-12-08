using FluentPOS.Shared.DTOs.Dtos;
using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Modules.Accounting.Core.Dtos
{
    public class PayrollTransactionDto : BaseEntityDto
    {
        public long PayrollId { get; set; }

        public TransactionType TransactionType { get; set; }

        public EntryType EntryType { get; set; }

        public string TransactionName { get; set; }

        public double Earning { get; set; }

        public double Deduction { get; set; }

        public double DaysOrHours { get; set; }

        public decimal PerDaySalary { get; set; }
    }
}
