﻿using FluentPOS.Shared.DTOs.Dtos;
using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Modules.Accounting.Core.Dtos
{
    public class SalaryPerksDto : BaseEntityDto
    {
        public long? EmployeeId { get; set; }

        public string Name { get; set; }

        public SalaryPerksType Type { get; set; }

        public decimal Percentage { get; set; }

        public decimal Amount { get; set; }

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
