using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Modules.Accounting.Core.Entities
{
    public class PayrollRequest : BaseEntity
    {

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Month { get; set; }

        public PayPeriod PayPeriod { get; set; }

        public SalaryCalculationFormula SalaryCalculationFormula { get; set; }

        public bool IgnoreAttendance { get; set; }

        public bool IgnoreDeductionForAbsents { get; set; }

        public bool IgnoreDeductionForLateComer { get; set; }

        public bool IgnoreOvertime { get; set; }

        public string Status { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public string Logs { get; set; }

        public string Message { get; set; }
    }
}