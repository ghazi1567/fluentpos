using System;

namespace FluentPOS.Shared.DTOs.Organizations.Departments
{
    public record GetDepartmentResponse(long Id,
DateTimeOffset? CreatedAt,
DateTimeOffset? UpdatedAt,
long OrganizationId,
long BranchId,
string Name,
bool IsGlobalDepartment,
string Description,
long? HeadOfDepartment,
int Production,
long PolicyId,
long? ParentId);
}