using FluentPOS.Shared.DTOs.Dtos;
using FluentPOS.Shared.DTOs.Dtos.Organizations;
using FluentPOS.Shared.DTOs.Dtos.Peoples;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Accounting.Core.Dtos
{
    public class PayrollRequestDto : BaseEntityDto
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

        public List<Guid> EmployeeIds { get; set; }

        public PolicyDto Policy { get; set; }

        public EmployeeDto EmployeeInfo { get; set; }

        public SalaryDto EmployeeSalary { get; set; }
    }
}
