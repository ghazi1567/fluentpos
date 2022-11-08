using FluentPOS.Shared.DTOs.Dtos.Accounting;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services.Accounting
{
    public interface IPayrollService
    {
        Task GeneratePayroll(PayrollRequestDto payrollRequest);

        Task InsertBasicSalary(Guid employeeId, decimal BasicSalary);

        Task SalaryIncrement(Guid employeeId, decimal decrement);

        Task SalaryDecrement(Guid employeeId, decimal decrement);

        Task<bool> IsPayrollGenerated(DateTime dateTime);
        Task<bool> IsPayrollGenerated(Guid employeeId, DateTime dateTime);

    }
}