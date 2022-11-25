using FluentPOS.Shared.DTOs.Dtos.Peoples;
using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IAttendanceService
    {
        Task<bool> MarkManualAttendance(Guid requestId);

        Task<bool> MarkOverTime(Guid requestId);

        Task<List<AttendanceDto>> GetEmployeeAttendance(List<Guid> employeeIds, DateTime startDate, DateTime endDate);

        bool TiggerAutoAbsentJob(DateTime? datetime);

        Task<bool> UpdateAttendance(AttendanceDto attendance);

        Task<bool> MarkNewAttendance(Guid employeeId, DateTime attendanceDate, AttendanceStatus attendanceStatus);

        Task<bool> IsAttendanceExist(Guid employeeId, DateTime attendanceDate);

        Task<bool> IsOverTimeExist(Guid employeeId, DateTime attendanceDate);

        Task<bool> MarkBioAttendance(int punchCode, DateTime attendanceDate);

        Task<bool> UpdateModification(Guid requestId);
    }
}