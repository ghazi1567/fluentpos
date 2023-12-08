using FluentPOS.Shared.DTOs.Dtos.Peoples;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IAttendanceService
    {
        Task<bool> MarkManualAttendance(long requestId);

        Task<bool> MarkOverTime(long requestId);

        Task<List<AttendanceDto>> GetEmployeeAttendance(List<long> employeeIds, DateTime startDate, DateTime endDate);

        bool TiggerAutoAbsentJob(DateTime? datetime);

        Task<bool> UpdateAttendance(AttendanceDto attendance);

        Task<bool> MarkNewAttendance(long employeeId, DateTime attendanceDate, AttendanceStatus attendanceStatus);

        Task<bool> IsAttendanceExist(long employeeId, DateTime attendanceDate);

        Task<bool> IsOverTimeExist(long employeeId, DateTime attendanceDate);

        Task<bool> MarkBioAttendance(int punchCode, DateTime attendanceDate);

        Task<bool> UpdateModification(long requestId);

        bool TiggerAutoPresentJob(DateTime? datetime, bool isCheckIn = true);

        Task<bool> DeleteAttendanceOrOvertime(long requestId);
    }
}