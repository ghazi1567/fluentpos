using FluentPOS.Shared.DTOs.Dtos.Organizations;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services.Organization
{
    public interface IOrgService
    {
        OrgDetailsDto GetOrgDetails(Guid Id);

        Task<OrgDetailsDto> GetPolicyDetailsAsync(Guid Id, bool IncludeAllDetails = false);
    }
}