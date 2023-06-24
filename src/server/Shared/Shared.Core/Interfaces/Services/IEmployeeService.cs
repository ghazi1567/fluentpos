using FluentPOS.Shared.DTOs.Dtos.Peoples;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<List<Shared.DTOs.Dtos.Peoples.EmployeeDto>> GetEmployeeListAsync();

        Task<EmployeeDto> GetEmployeeDetailsAsync(Guid Id);

        Task<List<Shared.DTOs.Dtos.Peoples.EmployeeDto>> GetEmployeeDetailsAsync(List<Guid> Ids);

        Task<List<Shared.DTOs.Dtos.Peoples.EmployeeDto>> GetEmployeeListByPolicyAsync(List<Guid> Ids);

        Task<List<Shared.DTOs.Dtos.Peoples.EmployeeDto>> GetMyReporterEmployeeListAsync(Guid id, bool includeMe = false);

        Task<int> GetEmployeeCountAsync(bool isActiveOnly);

        Task<List<Shared.DTOs.Dtos.Peoples.EmployeeDto>> GetEmployeeListByPayPeriodAsync(PayPeriod payPeriod);

    }
}