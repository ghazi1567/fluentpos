using System;

namespace FluentPOS.Shared.DTOs.Organizations.Designations
{
    public record GetDesignationResponse(Guid Id,
DateTime? CreateaAt,
DateTime? UpdatedAt,
Guid OrganizationId,
Guid BranchId,
string Name,
Guid DepartmentId);
}
