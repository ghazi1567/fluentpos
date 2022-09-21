using FluentPOS.Shared.DTOs.Dtos.Peoples;
using FluentPOS.Shared.DTOs.People.EmployeeRequests;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetEmployeeDetailsAsync(Guid Id);
    }
}