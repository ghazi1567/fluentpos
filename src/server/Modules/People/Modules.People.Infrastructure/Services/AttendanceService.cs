﻿using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Interfaces.Services.Identity;
using FluentPOS.Shared.Core.Interfaces.Services.Organization;
using FluentPOS.Shared.DTOs.Dtos.Peoples;
using FluentPOS.Shared.DTOs.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Infrastructure.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly IOrgService _orgService;
        private readonly ICurrentUser _currentUser;
        private readonly IStringLocalizer<AttendanceService> _localizer;

        public AttendanceService(IPeopleDbContext context, IMapper mapper, IEmployeeService employeeService, IOrgService orgService, ICurrentUser currentUser, IStringLocalizer<AttendanceService> localizer)
        {
            _context = context;
            _mapper = mapper;
            _employeeService = employeeService;
            _orgService = orgService;
            _currentUser = currentUser;
            _localizer = localizer;
        }


        public async Task<bool> IsAttendanceExist(Guid employeeId, DateTime attendanceDate)
        {
            return await _context.Attendances.AnyAsync(x => x.EmployeeId == employeeId && x.AttendanceDate.Date == attendanceDate.Date && x.AttendanceType != AttendanceType.OverTime);
        }

        public async Task<bool> IsOverTimeExist(Guid employeeId, DateTime attendanceDate)
        {
            return await _context.Attendances.AnyAsync(x => x.EmployeeId == employeeId && x.AttendanceDate.Date == attendanceDate.Date && x.AttendanceType == AttendanceType.OverTime);
        }

        public async Task<bool> MarkBioAttendance(int punchCode, DateTime attendanceDate)
        {
            var employeeInfo = _context.Employees.FirstOrDefault(x => x.PunchCode == punchCode);
            if (employeeInfo != null)
            {
                bool isExist = await IsAttendanceExist(employeeInfo.Id, attendanceDate.Date);
                Attendance attendance = null;
                if (isExist)
                {
                    attendance = await _context.Attendances.FirstOrDefaultAsync(x => x.EmployeeId == employeeInfo.Id && x.AttendanceDate.Date == attendanceDate.Date && x.AttendanceType != AttendanceType.OverTime);
                }

                attendance = new Attendance
                {
                    EmployeeId = employeeInfo.Id,
                    DepartmentId = employeeInfo.DepartmentId,
                    DesignationId = employeeInfo.DesignationId,
                    AttendanceDate = attendanceDate.Date,
                    CheckIn = attendanceDate.TimeOfDay,
                    CheckOut = TimeSpan.Zero,
                    ActualIn = attendanceDate.TimeOfDay,
                    ActualOut = TimeSpan.Zero,
                    AttendanceType = Shared.DTOs.Enums.AttendanceType.Bio,
                    BranchId = employeeInfo.BranchId,
                    CreateaAt = DateTime.Now,
                    EarnedHours = 0,
                    BioMachineId = string.Empty,
                    EarnedMinutes = 0,
                    ExpectedIn = TimeSpan.Zero,
                    ExpectedOut = TimeSpan.Zero,
                    OrganizationId = employeeInfo.OrganizationId,
                    OvertimeHours = 0,
                    OvertimeMinutes = 0,
                    PolicyId = employeeInfo.PolicyId,
                    Reason = "Bio Attendance",
                    AttendanceStatus = Shared.DTOs.Enums.AttendanceStatus.Present
                };

                if (attendance.CheckIn != TimeSpan.Zero && attendance.CheckOut != TimeSpan.Zero)
                {
                    attendance = await CalculateEarnedHours(attendance);
                }


                var entity = _context.Attendances.Add(attendance);
                return true;
            }

            return false;
        }


        public async Task<bool> MarkManualAttendance(Guid requestId)
        {
            var request = _context.EmployeeRequests.AsNoTracking().Where(x => x.Id == requestId).FirstOrDefault();
            if (request != null)
            {
                bool isExist = await IsAttendanceExist(request.EmployeeId, request.AttendanceDate.Date);
                if (isExist)
                {
                    throw new PeopleException(_localizer[$"Attendance Already Marked For Date : {request.AttendanceDate}!"], HttpStatusCode.Ambiguous);
                }


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

        public async Task<bool> MarkNewAttendance(Guid employeeId, DateTime attendanceDate, AttendanceStatus attendanceStatus)
        {
            var attendance = new Attendance();
            var employeeInfo = await _employeeService.GetEmployeeDetailsAsync(employeeId);
            if (employeeInfo != null)
            {
                attendance.EmployeeId = employeeId;
                attendance.DepartmentId = employeeInfo.DepartmentId;
                attendance.DesignationId = employeeInfo.DesignationId;
                attendance.BranchId = employeeInfo.BranchId;
                attendance.OrganizationId = employeeInfo.OrganizationId;
                attendance.AttendanceDate = attendanceDate;
                attendance.AttendanceStatus = attendanceStatus;
                attendance.AddedOn = DateTime.Now;
                attendance.AttendanceType = AttendanceType.Manual;
                attendance.CreateaAt = DateTime.Now;
                attendance.ApprovedBy = _currentUser.GetUserId();
                if (attendanceStatus == AttendanceStatus.Present)
                {
                    var policy = await _orgService.GetPolicyDetailsAsync(employeeInfo.PolicyId);
                    if (policy != null && policy.Policy != null)
                    {
                        attendance.CheckIn = policy.Policy.ShiftStartTime;
                        attendance.CheckOut = policy.Policy.ShiftEndTime;
                    }
                }

                attendance = await CalculateEarnedHours(attendance, true);
                var entity = await _context.Attendances.AddAsync(attendance);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAttendance(AttendanceDto attendanceDto)
        {
            if (attendanceDto != null)
            {
                var attendance = await _context.Attendances.Where(c => c.Id == attendanceDto.Id).AsNoTracking().FirstOrDefaultAsync();
                if (attendance != null)
                {
                    attendance.AttendanceStatus = attendanceDto.AttendanceStatus;
                    attendance.CheckIn = attendanceDto.CheckIn;
                    attendance.CheckOut = attendanceDto.CheckOut;
                    attendance.Reason = attendanceDto.Reason;
                    attendance = await CalculateEarnedHours(attendance, true);
                    var entity = _context.Attendances.Update(attendance);
                    await _context.SaveChangesAsync();
                }

                return true;
            }

            return false;
        }

        public async Task<bool> MarkOverTime(Guid requestId)
        {
            var request = _context.EmployeeRequests.AsNoTracking().Where(x => x.Id == requestId).FirstOrDefault();
            if (request != null)
            {
                bool isExist = await IsOverTimeExist(request.EmployeeId, request.AttendanceDate.Date);
                if (isExist)
                {
                    throw new PeopleException(_localizer[$"Attendance Already Marked For Date : {request.AttendanceDate}!"], HttpStatusCode.Ambiguous);
                }
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

        public async Task<Attendance> CalculateEarnedHours(Attendance attendance, bool IsUpdate = false)
        {
            if (attendance.AttendanceType == Shared.DTOs.Enums.AttendanceType.Manual || attendance.AttendanceType == Shared.DTOs.Enums.AttendanceType.Bio || attendance.AttendanceType == Shared.DTOs.Enums.AttendanceType.System)
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
                if (IsOffDay(attendance.AttendanceDate, policy.Policy) && !IsUpdate)
                {
                    attendance.AttendanceStatus = Shared.DTOs.Enums.AttendanceStatus.Off;
                }

                if (attendance.AttendanceType != Shared.DTOs.Enums.AttendanceType.OverTime)
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

                    if (policy.Policy.EarnedHourPolicy == Shared.DTOs.Enums.EarnedHourPolicy.ShiftHour && policy.Policy.DailyWorkingHour > 0 && policy.Policy.DailyWorkingHour < attendance.EarnedHours)
                    {
                        attendance.EarnedHours = policy.Policy.DailyWorkingHour;
                    }
                }
            }

            if (attendance.AttendanceStatus == Shared.DTOs.Enums.AttendanceStatus.Absent || attendance.AttendanceStatus == Shared.DTOs.Enums.AttendanceStatus.Off)
            {
                attendance.DeductedHours = 0;
                attendance.LateMinutes = 0;
                attendance.OvertimeHours = 0;
                attendance.EarnedHours = 0;
                attendance.ActualEarnedHours = 0;
                attendance.EarnedMinutes = 0;
            }


            attendance.TotalEarnedHours = attendance.EarnedHours + attendance.OvertimeHours;
            return attendance;
        }

        public async Task<List<AttendanceDto>> GetEmployeeAttendance(List<Guid> employeeIds, DateTime startDate, DateTime endDate)
        {
            var entity = await _context.Attendances.AsNoTracking().Where(x => employeeIds.Contains(x.EmployeeId) && x.AttendanceDate.Date >= startDate.Date && x.AttendanceDate.Date <= endDate.Date).ToListAsync();
            return _mapper.Map<List<AttendanceDto>>(entity);
        }

        public async Task<bool> MarkAutoAttendance(DateTime dateTime)
        {
            var request = new EmployeeRequest();
            if (request != null)
            {
                var employeeInfo = await _employeeService.GetEmployeeDetailsAsync(request.EmployeeId);

                var attendance = new Attendance
                {
                    EmployeeId = request.EmployeeId,
                    DepartmentId = request.DepartmentId,
                    DesignationId = request.DesignationId,
                    AttendanceDate = dateTime,
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

        public async Task<bool> MarkAutoAbsentOrOffDay(DateTime dateTime)
        {
            List<Shared.DTOs.Dtos.Organizations.PolicyDto> policyList = new List<Shared.DTOs.Dtos.Organizations.PolicyDto>();
            var employeList = await _employeeService.GetEmployeeListAsync();

            foreach (var item in employeList)
            {
                if (_context.Attendances.Any(x => x.EmployeeId == item.Id && x.AttendanceDate.Date == dateTime.Date))
                {
                    continue;
                }

                if (!policyList.Any(x => x.Id == item.PolicyId))
                {
                    var policyDetail = await _orgService.GetPolicyDetailsAsync(item.PolicyId);
                    if (policyDetail != null)
                    {
                        policyList.Add(policyDetail.Policy);
                    }
                }

                var policy = policyList.FirstOrDefault(x => x.Id == item.PolicyId);

                var status = Shared.DTOs.Enums.AttendanceStatus.None;

                if (IsOffDay(dateTime, policy))
                {
                    if (IsSandwich(dateTime, policy, item.Id.Value))
                    {
                        status = Shared.DTOs.Enums.AttendanceStatus.Absent;
                    }
                    else
                    {
                        status = Shared.DTOs.Enums.AttendanceStatus.Off;
                    }
                }
                else
                {
                    status = Shared.DTOs.Enums.AttendanceStatus.Absent;
                }

                var attendance = new Attendance
                {
                    EmployeeId = item.Id.Value,
                    DepartmentId = item.DepartmentId,
                    DesignationId = item.DesignationId,
                    AttendanceDate = dateTime,
                    ActualIn = policy.ShiftStartTime,
                    ActualOut = policy.ShiftEndTime,
                    AddedOn = DateTime.Now,
                    AttendanceType = Shared.DTOs.Enums.AttendanceType.System,
                    BranchId = item.BranchId,
                    CreateaAt = DateTime.Now,
                    EarnedHours = 0,
                    BioMachineId = string.Empty,
                    EarnedMinutes = 0,
                    ExpectedIn = TimeSpan.Zero,
                    ExpectedOut = TimeSpan.Zero,
                    OrganizationId = item.OrganizationId,
                    OvertimeHours = 0,
                    OvertimeMinutes = 0,
                    PolicyId = item.PolicyId,
                    Reason = "Sytem Updated",
                    Status = Shared.DTOs.Enums.RequestStatus.Approved,
                    StatusUpdateOn = DateTime.Now,
                    AttendanceStatus = status,
                };

                var entity = _context.Attendances.Add(attendance);
                _context.SaveChanges();
            }

            return false;
        }

        public bool TiggerAutoAbsentJob(DateTime? datetime)
        {
            var dt = new DateTime(2022, 08, 31, 00, 00, 00);
            for (int i = 0; i <= 30; i++)
            {
                dt = dt.AddDays(1);
                bool result = MarkAutoAbsentOrOffDay(dt).Result;
            }

            // if (datetime == null)
            // {
            //     datetime = DateTime.Now;
            // }
            // try
            // {
            //     //bool result = MarkAutoAbsentOrOffDay(datetime.Value).Result;
            // }
            // catch (Exception ex)
            // {
            //     throw ex;
            // }
            return true;
        }

        private bool IsOffDay(DateTime dateTime, Shared.DTOs.Dtos.Organizations.PolicyDto policy)
        {
            bool isWorkingDay = false;
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    isWorkingDay = policy.IsSunday;
                    break;
                case DayOfWeek.Monday:
                    isWorkingDay = policy.IsMonday;
                    break;
                case DayOfWeek.Tuesday:
                    isWorkingDay = policy.IsTuesday;
                    break;
                case DayOfWeek.Wednesday:
                    isWorkingDay = policy.IsWednesday;
                    break;
                case DayOfWeek.Thursday:
                    isWorkingDay = policy.IsThursday;
                    break;
                case DayOfWeek.Friday:
                    isWorkingDay = policy.IsFriday;
                    break;
                case DayOfWeek.Saturday:
                    isWorkingDay = policy.IsSaturday;
                    break;
                default:
                    break;
            }

            return !isWorkingDay;
        }

        private bool IsSandwich(DateTime dateTime, Shared.DTOs.Dtos.Organizations.PolicyDto policy, Guid employeeId)
        {
            if (policy.SandwichLeaveCount == 0)
            {
                return false;
            }

            int absentCount = 0;
            var lastDate = dateTime;
            for (int i = policy.SandwichLeaveCount; i > 0; i--)
            {
                lastDate = lastDate.AddDays(-1);
                var attendance = _context.Attendances.FirstOrDefault(x => x.AttendanceDate.Date == lastDate.Date && x.EmployeeId == employeeId);
                if (attendance != null)
                {
                    if (attendance.AttendanceStatus == Shared.DTOs.Enums.AttendanceStatus.Absent)
                    {
                        absentCount++;
                    }
                    else
                    {
                        absentCount--;
                    }
                }
            }

            return absentCount == policy.SandwichLeaveCount;
        }
    }
}