using System;

namespace FluentPOS.Shared.DTOs.Organizations.Designations
{
    public record GetDesignationResponse(long Id,
DateTimeOffset? CreatedAt,
DateTimeOffset? UpdatedAt,
long OrganizationId,
long BranchId,
string Name,
long DepartmentId);
}
