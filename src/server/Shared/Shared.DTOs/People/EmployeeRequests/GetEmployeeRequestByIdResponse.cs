using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Shared.DTOs.People.EmployeeRequests
{
    public record GetEmployeeRequestByIdResponse(Guid Id,

DateTime? CreateaAt,

DateTime? UpdatedAt,

Guid OrganizationId,

Guid BranchId,

Guid UserId,
Guid EmployeeId,

Guid DepartmentId,

Guid PolicyId,

Guid DesignationId,

string RequestType,

DateTime RequestedOn,

Guid RequestedBy,

DateTime AttendanceDate,

DateTime? CheckIn,

DateTime? CheckOut,

int OvertimeHours,

OverTimeType OverTimeType,

string Reason);
}
