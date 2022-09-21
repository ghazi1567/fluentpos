using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Shared.DTOs.People.EmployeeRequests
{
    public record GetEmployeeRequestsResponse(Guid Id,

DateTime? CreateaAt,

DateTime? UpdatedAt,

Guid OrganizationId,

Guid BranchId,

Guid UserId,
Guid EmployeeId,

Guid DepartmentId,

Guid PolicyId,

Guid DesignationId,

RequestType RequestType,

DateTime RequestedOn,

Guid RequestedBy,

DateTime AttendanceDate,

TimeSpan? CheckIn,

TimeSpan? CheckOut,

int OvertimeHours,

string OverTimeType,

string Reason);
}