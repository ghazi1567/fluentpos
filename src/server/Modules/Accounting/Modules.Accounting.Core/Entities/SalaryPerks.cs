using System;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Enums;

namespace FluentPOS.Modules.Accounting.Core.Entities
{
    public class SalaryPerks : BaseEntity
    {
        public long? EmployeeId { get; set; }

        public string Name { get; set; }

        public SalaryPerksType Type { get; set; }

        public double Percentage { get; set; }

        public double Amount { get; set; }

        public bool IsRecursion { get; set; }

        public bool IsRecursionUnLimited { get; set; }

        public DateTime? RecursionEndMonth { get; set; }

        public string Description { get; set; }

        public DateTime EffecitveFrom { get; set; }

        public bool IsTaxable { get; set; }

        public bool IsGlobal { get; set; }

        public GlobalSalaryPerksType GlobalPerkType { get; set; }
    }
}