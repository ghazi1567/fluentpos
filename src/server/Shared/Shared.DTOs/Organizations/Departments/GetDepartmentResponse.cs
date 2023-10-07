using System;

namespace FluentPOS.Shared.DTOs.Organizations.Departments
{
    public record GetDepartmentResponse(Guid Id,
DateTime? CreateaAt,
DateTime? UpdatedAt,
Guid OrganizationId,
Guid BranchId,
string Name,
bool IsGlobalDepartment,
string Description,
Guid? HeadOfDepartment,
int Production,
Guid PolicyId,
Guid? ParentId);
}