using FluentPOS.Shared.DTOs.Dtos.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services.Accounting
{
    public interface IPayrollService
    {
        Task GeneratePayroll(PayrollRequestDto payrollRequest);
    }
}