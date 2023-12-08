using FluentPOS.Shared.DTOs.Dtos.Peoples;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>> GetEmployeeListAsync();

        Task<BaseEmployeeDto> GetEmployeeDetailsAsync(long Id);

        Task<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>> GetEmployeeDetailsAsync(List<long> Ids);

        Task<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>> GetEmployeeListByPolicyAsync(List<long> Ids);

        Task<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>> GetMyReporterEmployeeListAsync(long id, bool includeMe = false);

        Task<int> GetEmployeeCountAsync(bool isActiveOnly);

        Task<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>> GetEmployeeListByPayPeriodAsync(PayPeriod payPeriod);

    }
}