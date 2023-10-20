using System;

namespace FluentPOS.Shared.DTOs.Organizations.Designations
{
    public record GetDesignationResponse(Guid Id,
DateTimeOffset? CreatedAt,
DateTimeOffset? UpdatedAt,
Guid OrganizationId,
Guid BranchId,
string Name,
Guid DepartmentId);
}
