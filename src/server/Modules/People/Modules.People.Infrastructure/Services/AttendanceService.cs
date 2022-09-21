using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Interfaces.Services.Organization;
using FluentPOS.Shared.DTOs.Dtos.Peoples;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Infrastructure.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly IOrgService _orgService;

        public AttendanceService(IPeopleDbContext context, IMapper mapper, IEmployeeService employeeService, IOrgService orgService)
        {
            _context = context;
            _mapper = mapper;
            _employeeService = employeeService;
            _orgService = orgService;
        }

        public async Task<bool> MarkManualAttendance(Guid requestId)
        {
            var request = _context.EmployeeRequests.AsNoTracking().Where(x => x.Id == requestId).FirstOrDefault();
            if (request != null)
            {
                var employeeInfo = await _employeeService.GetEmployeeDetailsAsync(request.EmployeeId);

                var attendance = new Attendance
                {
                    EmployeeId = request.EmployeeId,
                    DepartmentId = request.DepartmentId,
                    DesignationId = request.DesignationId,
                    AttendanceDate = request.AttendanceDate,
                    CheckIn = request.CheckIn.Value,
                    CheckOut = request.CheckOut.Value,
                    ActualIn = request.CheckIn.Value,
                    ActualOut = request.CheckOut.Value,
                    AddedOn = request.RequestedOn,
                    ApprovedBy = request.AssignedTo.Value,
                    AttendanceType = Shared.DTOs.Enums.AttendanceType.Manual,
                    BranchId = request.BranchId,
                    CreateaAt = DateTime.Now,
                    EarnedHours = 0,
                    BioMachineId = string.Empty,
                    EarnedMinutes = 0,
                    ExpectedIn = TimeSpan.Zero,
                    ExpectedOut = TimeSpan.Zero,
                    OrganizationId = request.OrganizationId,
                    OvertimeHours = 0,
                    OvertimeMinutes = 0,
                    PolicyId = employeeInfo.PolicyId,
                    Reason = request.Reason,
                    RequestId = requestId,
                    Status = request.Status,
                    StatusUpdateOn = request.StatusUpdateOn,
                    AttendanceStatus = Shared.DTOs.Enums.AttendanceStatus.Present
                };

                attendance = await CalculateEarnedHours(attendance);

                var entity = _context.Attendances.Add(attendance);
                return true;
            }

            return false;
        }

        public async Task<bool> MarkOverTime(Guid requestId)
        {
            var request = _context.EmployeeRequests.AsNoTracking().Where(x => x.Id == requestId).FirstOrDefault();
            if (request != null)
            {
                var employeeInfo = await _employeeService.GetEmployeeDetailsAsync(request.EmployeeId);

                var attendance = new Attendance
                {
                    EmployeeId = request.EmployeeId,
                    DepartmentId = request.DepartmentId,
                    DesignationId = request.DesignationId,
                    AttendanceDate = request.AttendanceDate,
                    CheckIn = request.CheckIn.Value,
                    CheckOut = request.CheckOut.Value,
                    ActualIn = request.CheckIn.Value,
                    ActualOut = request.CheckOut.Value,
                    AddedOn = request.RequestedOn,
                    ApprovedBy = request.AssignedTo.Value,
                    AttendanceType = Shared.DTOs.Enums.AttendanceType.OverTime,
                    BranchId = request.BranchId,
                    CreateaAt = DateTime.Now,
                    EarnedHours = 0,
                    BioMachineId = string.Empty,
                    EarnedMinutes = 0,
                    ExpectedIn = TimeSpan.Zero,
                    ExpectedOut = TimeSpan.Zero,
                    OrganizationId = request.OrganizationId,
                    OvertimeHours = 0,
                    OvertimeMinutes = 0,
                    PolicyId = employeeInfo.PolicyId,
                    Reason = request.Reason,
                    RequestId = requestId,
                    Status = request.Status,
                    StatusUpdateOn = request.StatusUpdateOn
                };

                attendance = await CalculateEarnedHours(attendance);

                var entity = _context.Attendances.Add(attendance);
                return true;
            }

            return false;
        }

        public async Task<Attendance> CalculateEarnedHours(Attendance attendance)
        {
            if (attendance.AttendanceType == Shared.DTOs.Enums.AttendanceType.Manual || attendance.AttendanceType == Shared.DTOs.Enums.AttendanceType.Bio)
            {
                var timeDiffrence = attendance.CheckOut - attendance.CheckIn;
                attendance.EarnedHours = Math.Round(timeDiffrence.TotalHours, 2);
                attendance.ActualEarnedHours = attendance.EarnedHours;
                attendance.EarnedMinutes = timeDiffrence.Minutes;
            }
            else if (attendance.AttendanceType == Shared.DTOs.Enums.AttendanceType.OverTime)
            {
                var overtimeDiffrence = attendance.CheckOut - attendance.CheckIn;
                attendance.OvertimeHours = Math.Round(overtimeDiffrence.TotalHours, 2);
                attendance.OvertimeMinutes = overtimeDiffrence.Minutes;
            }

            // apply policy rules
            var policy = await _orgService.GetPolicyDetailsAsync(attendance.PolicyId);
            if (policy.Policy != null)
            {
                if (attendance.AttendanceType == Shared.DTOs.Enums.AttendanceType.Manual || attendance.AttendanceType == Shared.DTOs.Enums.AttendanceType.Bio)
                {
                    attendance.ExpectedIn = policy.Policy.ShiftStartTime;
                    attendance.ExpectedOut = policy.Policy.ShiftEndTime;

                    if (policy.Policy.lateComersPenaltyType == Shared.DTOs.Enums.LateComersPenaltyType.Hour)
                    {
                        var timeDiff = attendance.CheckIn - attendance.ExpectedIn;

                        if (timeDiff.TotalMinutes > policy.Policy.AllowedLateMinutes)
                        {
                            attendance.DeductedHours = policy.Policy.lateComersPenalty;
                            attendance.EarnedHours -= attendance.DeductedHours;
                            attendance.LateMinutes = timeDiff.Minutes;
                        }
                    }

                    if (policy.Policy.EarlyArrivalPolicy == Shared.DTOs.Enums.EarlyArrivalPolicy.ShiftTime)
                    {
                        var checkInDiff = attendance.ExpectedIn - attendance.CheckIn;
                        if (checkInDiff.TotalMinutes > 0)
                        {
                            attendance.CheckIn = policy.Policy.ShiftStartTime;
                        }

                        // var checkOutDiff = attendance.ExpectedOut - attendance.CheckOut;
                        // if (checkInDiff.TotalMinutes < 0)
                        // {
                        //     attendance.CheckOut = policy.Policy.ShiftEndTime;
                        // }
                    }

                    if (policy.Policy.EarnedHourPolicy == Shared.DTOs.Enums.EarnedHourPolicy.ShiftHour)
                    {
                        var timeDiffrence = attendance.ExpectedOut - attendance.ExpectedIn;
                        if (timeDiffrence.Hours < attendance.EarnedHours)
                        {
                            attendance.EarnedHours = Math.Round(timeDiffrence.TotalHours, 2);
                        }
                    }
                }
            }

            attendance.TotalEarnedHours = attendance.EarnedHours + attendance.OvertimeHours;
            return attendance;
        }

        public async Task<List<AttendanceDto>> GetEmployeeAttendance(List<Guid> employeeIds, DateTime startDate,DateTime endDate)
        {
            var entity = await _context.Attendances.AsNoTracking().Where(x => employeeIds.Contains(x.EmployeeId) && x.AttendanceDate >= startDate && x.AttendanceDate <= endDate).ToListAsync();
            return _mapper.Map<List<AttendanceDto>>(entity);
        }
    }
}