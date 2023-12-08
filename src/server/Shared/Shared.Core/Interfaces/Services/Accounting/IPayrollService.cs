using FluentPOS.Shared.DTOs.Dtos.Accounting;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services.Accounting
{
    public interface IPayrollService
    {
        Task GeneratePayroll(PayrollRequestDto payrollRequest);

        Task InsertBasicSalary(long employeeId, decimal BasicSalary);

        Task SalaryIncrement(long employeeId, decimal decrement);

        Task SalaryDecrement(long employeeId, decimal decrement);

        Task<bool> IsPayrollGenerated(DateTime dateTime);
        Task<bool> IsPayrollGenerated(long employeeId, DateTime dateTime);

    }
}