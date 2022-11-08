using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Shared.DTOs.Organizations.Policies
{
    public record GetPolicyResponse(Guid Id,
DateTime? CreateaAt,
DateTime? UpdatedAt,
Guid OrganizationId,
Guid BranchId,
string Name,
Guid? DepartmentId,
PayslipType PayslipType,
PayPeriod PayPeriod,
int AllowedOffDays,
int? RequiredWorkingHour,
TimeSpan ShiftStartTime,
TimeSpan ShiftEndTime,
int AllowedLateMinutes,
int AllowedLateMinInMonth,
EarlyArrivalPolicy EarlyArrivalPolicy,
int ForceTimeout,
TimeoutPolicy TimeoutPolicy,
bool IsMonday,
bool IsTuesday,
bool IsWednesday,
bool IsThursday,
bool IsFriday,
bool IsSaturday,
bool IsSunday,
OverTime DailyOverTime,
OverTime HolidayOverTime,
LateComersPenaltyType lateComersPenaltyType,
int lateComersPenalty,
int DailyOverTimeRate,
int HolidayOverTimeRate,
EarnedHourPolicy earnedHourPolicy,
int SandwichLeaveCount,
int DailyWorkingHour
);
}
