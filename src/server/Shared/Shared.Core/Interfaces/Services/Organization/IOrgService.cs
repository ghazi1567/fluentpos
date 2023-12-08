using FluentPOS.Shared.DTOs.Dtos.Organizations;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services.Organization
{
    public interface IOrgService
    {
        OrgDetailsDto GetOrgDetails(long Id);

        Task<OrgDetailsDto> GetPolicyDetailsAsync(long Id, bool IncludeAllDetails = false);

        Task<List<PolicyDto>> GetPolicyDetailsAsync(PayPeriod payPeriod);

        Task<List<PolicyDto>> GetAllPoliciesAsync();

        Task<List<DepartmentDto>> GetDepartmentListAsync(List<long> Ids);

        Task<DepartmentDto> GetDepartmentByIdAsync(long id);

        Task<List<DepartmentDto>> GetAllDepartmentAsync();
    }
}