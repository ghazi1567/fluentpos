using FluentPOS.Shared.DTOs.Dtos.Organizations;
using FluentPOS.Shared.DTOs.Dtos.Peoples;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Dtos.Accounting
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

        public List<Guid>? EmployeeIds { get; set; }

        public PolicyDto? Policy { get; set; }

        public EmployeeDto? EmployeeInfo { get; set; }

        public SalaryDto? EmployeeSalary { get; set; }

        public string Status { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public List<string> Logs { get; set; }

    }
}
