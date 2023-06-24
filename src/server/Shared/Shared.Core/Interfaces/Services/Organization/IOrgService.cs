using FluentPOS.Shared.DTOs.Dtos.Organizations;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services.Organization
{
    public interface IOrgService
    {
        OrgDetailsDto GetOrgDetails(Guid Id);

        Task<OrgDetailsDto> GetPolicyDetailsAsync(Guid Id, bool IncludeAllDetails = false);

        Task<List<PolicyDto>> GetPolicyDetailsAsync(PayPeriod payPeriod);

        Task<List<PolicyDto>> GetAllPoliciesAsync();

        Task<List<DepartmentDto>> GetDepartmentListAsync(List<Guid> Ids);

        Task<DepartmentDto> GetDepartmentByIdAsync(Guid id);

        Task<List<DepartmentDto>> GetAllDepartmentAsync();
    }
}