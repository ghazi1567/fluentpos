using FluentPOS.Shared.DTOs.Dtos;
using System;

namespace FluentPOS.Modules.Accounting.Core.Dtos
{
    public class SalaryDto : Shared.DTOs.Dtos.Accounting.SalaryDto
    {
        public string EmployeeName { get; set; }

        public decimal CurrentSalary { get; set; }
    }
}