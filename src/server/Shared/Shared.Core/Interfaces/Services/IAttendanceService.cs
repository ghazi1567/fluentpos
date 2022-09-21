using FluentPOS.Shared.DTOs.Dtos.Peoples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IAttendanceService
    {
        Task<bool> MarkManualAttendance(Guid requestId);

        Task<bool> MarkOverTime(Guid requestId);

        Task<List<AttendanceDto>> GetEmployeeAttendance(List<Guid> employeeIds, DateTime startDate, DateTime endDate);
    }
}