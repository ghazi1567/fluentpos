using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Shared.DTOs.People.EmployeeRequests
{
    public record GetEmployeeRequestByIdResponse(long Id,

DateTimeOffset? CreatedAt,

DateTimeOffset? UpdatedAt,

long OrganizationId,

long BranchId,

long UserId,
long EmployeeId,

long DepartmentId,

long PolicyId,

long DesignationId,

string RequestType,

DateTime RequestedOn,

long RequestedBy,

DateTime AttendanceDate,

DateTime? CheckIn,

DateTime? CheckOut,

int OvertimeHours,

OverTimeType OverTimeType,

string Reason);
}
