using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Modules.Accounting.Core.Entities
{
    public class PayrollTransaction : BaseEntity
    {
        public Guid PayrollId { get; set; }

        public TransactionType TransactionType { get; set; }

        public EntryType EntryType { get; set; }

        public string TransactionName { get; set; }

        public double Earning { get; set; }

        public double Deduction { get; set; }

        public double DaysOrHours { get; set; }

        public decimal PerDaySalary { get; set; }

    }
}
