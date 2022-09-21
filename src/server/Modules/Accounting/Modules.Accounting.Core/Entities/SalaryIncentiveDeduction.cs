using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Accounting.Core.Entities
{
    public class SalaryIncentiveDeduction : BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public int Type { get; set; }

        public decimal Amount { get; set; }

        public bool IsRecursion { get; set; }

        public DateTime OneTimeMonth { get; set; }

        public bool IsRecursionUnLimited { get; set; }

        public DateTime RecursionEndMonth { get; set; }

        public string Description { get; set; }
    }
}