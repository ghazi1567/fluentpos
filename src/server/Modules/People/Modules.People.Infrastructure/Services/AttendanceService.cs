using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Modules.People.Infrastructure.Persistence;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Interfaces.Services.Identity;
using FluentPOS.Shared.Core.Interfaces.Services.Organization;
using FluentPOS.Shared.DTOs.Dtos.Peoples;
using FluentPOS.Shared.DTOs.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Infrastructure.Services
{

    public static class DateHelper
    {
        public static DateTime ToDatetime(this TimeSpan ts, DateTime dateTime, bool IsNextDay = false)
        {
            int day = IsNextDay ? dateTime.Day + 1 : dateTime.Day;
            return new DateTime(dateTime.Year, dateTime.Month, day, ts.Hours, ts.Minutes, ts.Seconds);
        }
    }

    public class AttendanceService : IAttendanceService
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly IOrgService _orgService;
        private readonly ICurrentUser _currentUser;
        private readonly IStringLocalizer<AttendanceService> _localizer;
        private readonly ILogger<AttendanceService> _logger;
        private readonly PeopleDbContext _peopleDbContext;

        public AttendanceService(
            IPeopleDbContext context,
            IMapper mapper,
            IEmployeeService employeeService,
            IOrgService orgService,
            ICurrentUser currentUser,
            IStringLocalizer<AttendanceService> localizer,
            ILogger<AttendanceService> logger,
            PeopleDbContext peopleDbContext)
        {
            _context = context;
            _mapper = mapper;
            _employeeService = employeeService;
            _orgService = orgService;
            _currentUser = currentUser;
            _localizer = localizer;
            _logger = logger;
            _peopleDbContext = peopleDbContext;
        }

        public async Task<bool> IsAttendanceExist(Guid employeeId, DateTime attendanceDate)
        {
            return await _context.Attendances.AnyAsync(x => x.EmployeeId == employeeId && x.AttendanceDate.Date == attendanceDate.Date && x.AttendanceType != AttendanceType.OverTime);
        }

        public async Task<bool> IsOverTimeExist(Guid employeeId, DateTime attendanceDate)
        {
            return await _context.Attendances.AnyAsync(x => x.EmployeeId == employeeId && x.AttendanceDate.Date == attendanceDate.Date && x.AttendanceType == AttendanceType.OverTime);
        }

        // Not in use now
        public async Task<bool> MarkBioAttendance(int punchCode, DateTime attendanceDate)
        {
            var employeeInfo = _context.Employees.FirstOrDefault(x => x.PunchCode == punchCode);
            if (employeeInfo != null)
            {
                bool isExist = await IsAttendanceExist(employeeInfo.Id, attendanceDate.Date);
                Attendance attendance = null;
                if (isExist)
                {
                    _logger.LogCritical($"Attendance Already Exist : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                    attendance = await _context.Attendances.FirstOrDefaultAsync(x => x.EmployeeId == employeeInfo.Id && x.AttendanceDate.Date == attendanceDate.Date && x.AttendanceType != AttendanceType.OverTime);

                    if (attendance.IsCheckOutMissing)
                    {
                        _logger.LogCritical($"Checkout Missing : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");

                        attendance.CheckOut = attendanceDate;
                        attendance.ActualOut = attendanceDate;
                    }
                }
                else
                {
                    attendance = new Attendance
                    {
                        EmployeeId = employeeInfo.Id,
                        DepartmentId = employeeInfo.DepartmentId,
                        DesignationId = employeeInfo.DesignationId,
                        AttendanceDate = attendanceDate.Date,
                        CheckIn = attendanceDate,
                        CheckOut = null,
                        ActualIn = attendanceDate,
                        AttendanceType = AttendanceType.Bio,
                        BranchId = employeeInfo.BranchId,
                        CreateaAt = DateTime.Now,
                        EarnedHours = 0,
                        BioMachineId = string.Empty,
                        EarnedMinutes = 0,
                        OrganizationId = employeeInfo.OrganizationId,
                        OvertimeHours = 0,
                        OvertimeMinutes = 0,
                        PolicyId = employeeInfo.PolicyId,
                        Reason = "Bio Attendance",
                        AttendanceStatus = AttendanceStatus.Present
                    };

                    _logger.LogCritical($"New Attendance Mark : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                }

                attendance.IsCheckOutMissing = attendance.CheckOut == null;
                if (attendance.CheckIn != null && attendance.CheckOut != null)
                {
                    attendance = await CalculateEarnedHours(attendance);
                    _logger.LogCritical($"Attendance Hours Calculated : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                }

                if (isExist)
                {
                    var entity = _context.Attendances.Update(attendance);
                    _logger.LogCritical($"Updating Attendance : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                }
                else
                {
                    var entity = _context.Attendances.Add(attendance);
                    _logger.LogCritical($"Inserting Attendance : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                }

                return true;
            }
            else
            {
                _logger.LogCritical($"Employee Not Found For Punch Code: {punchCode}");
            }

            return false;
        }

        private async Task<Guid> GetEmployeePolicyId(Guid employeeId)
        {
            var policyId = Guid.Empty;
            var employeeInfo = _context.Employees.AsNoTracking().FirstOrDefault(x => x.Id == employeeId);
            if (employeeInfo != null)
            {
                if (employeeInfo.PolicyId != default && employeeInfo.PolicyId != Guid.Empty)
                {
                    policyId = employeeInfo.PolicyId;
                }
                else
                {
                    var department = await _orgService.GetDepartmentByIdAsync(employeeInfo.DepartmentId);
                    if (department != null)
                    {
                        policyId = department.PolicyId;
                    }
                }
            }

            return policyId;
        }

        private async Task<Guid> GetEmployeePolicyId(Employee employeeInfo)
        {
            var policyId = Guid.Empty;
            if (employeeInfo != null)
            {
                if (employeeInfo.PolicyId != default && employeeInfo.PolicyId != Guid.Empty)
                {
                    policyId = employeeInfo.PolicyId;
                }
                else
                {
                    var department = await _orgService.GetDepartmentByIdAsync(employeeInfo.DepartmentId);
                    if (department != null)
                    {
                        policyId = department.PolicyId;
                    }
                }
            }

            return policyId;
        }

        private async Task<bool> MarkBioAttendance(BioAttendanceLog bioAttendanceLog, bool isCheckIn, Guid? attendanceId = null)
        {
            int punchCode = int.Parse(bioAttendanceLog.PunchCode);
            DateTime attendanceDate = bioAttendanceLog.AttendanceDateTime;

            var employeeInfo = _context.Employees.FirstOrDefault(x => x.PunchCode == punchCode);
            if (employeeInfo != null)
            {
                var policyId = await GetEmployeePolicyId(employeeInfo);
                var policy = await _orgService.GetPolicyDetailsAsync(policyId);
                Attendance attendance = null;
                bool isExist = false;
                if (attendanceId.HasValue && attendanceId != null)
                {
                    attendance = await _context.Attendances.FirstOrDefaultAsync(x => x.Id == attendanceId.Value);
                    isExist = true;
                }
                else
                {
                    isExist = await IsAttendanceExist(employeeInfo.Id, attendanceDate.Date);
                    if (isExist)
                    {
                        attendance = await _context.Attendances.FirstOrDefaultAsync(x => x.EmployeeId == employeeInfo.Id && x.AttendanceDate.Date == attendanceDate.Date && x.AttendanceType != AttendanceType.OverTime);
                    }
                }

                if (isExist && attendance != null)
                {
                    _logger.LogCritical($"Attendance Already Exist : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                    attendance.AttendanceStatus = AttendanceStatus.Present;
                    if (isCheckIn)
                    {
                        _logger.LogCritical($"CheckIn Job : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");

                        attendance.CheckIn = attendanceDate;
                        attendance.ActualIn = attendanceDate;
                    }
                    else if (attendance.IsCheckOutMissing)
                    {
                        _logger.LogCritical($"Checkout Missing : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");

                        attendance.CheckOut = attendanceDate;
                        attendance.ActualOut = attendanceDate;
                    }
                    else
                    {
                        _logger.LogCritical($"Checkout job : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");

                        attendance.CheckOut = attendanceDate;
                        attendance.ActualOut = attendanceDate;
                    }
                }
                else
                {
                    attendance = new Attendance
                    {
                        EmployeeId = employeeInfo.Id,
                        DepartmentId = employeeInfo.DepartmentId,
                        DesignationId = employeeInfo.DesignationId,
                        AttendanceDate = attendanceDate.Date,
                        CheckIn = attendanceDate,
                        CheckOut = null,
                        ActualIn = attendanceDate,
                        AttendanceType = AttendanceType.Bio,
                        BranchId = employeeInfo.BranchId,
                        CreateaAt = DateTime.Now,
                        EarnedHours = 0,
                        BioMachineId = string.Empty,
                        EarnedMinutes = 0,
                        OrganizationId = employeeInfo.OrganizationId,
                        OvertimeHours = 0,
                        OvertimeMinutes = 0,
                        PolicyId = policyId,
                        Reason = "Bio Attendance",
                        AttendanceStatus = AttendanceStatus.Present,
                        IsNextDay = policy != null && policy.Policy != null ? policy.Policy.IsNextDay : false,
                        PunchCode = bioAttendanceLog.PunchCode,
                        CardNo = bioAttendanceLog.CardNo
                    };

                    _logger.LogCritical($"New Attendance Mark : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                }

                attendance.IsCheckOutMissing = attendance.CheckOut == null;
                if (attendance.CheckIn != null && attendance.CheckOut != null)
                {
                    attendance = await CalculateEarnedHours(attendance);
                    _logger.LogCritical($"Attendance Hours Calculated : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                }

                if (isExist)
                {
                    var entity = _context.Attendances.Update(attendance);
                    _logger.LogCritical($"Updating Attendance : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                }
                else
                {
                    var entity = _context.Attendances.Add(attendance);
                    _logger.LogCritical($"Inserting Attendance : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                }

                return true;
            }
            else
            {
                _logger.LogCritical($"Employee Not Found For Punch Code: {punchCode}");
            }

            return false;
        }

        // Using for manual attendance approval
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
                var policyId = await GetEmployeePolicyId(request.EmployeeId);
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
                    AttendanceType = AttendanceType.Manual,
                    BranchId = request.BranchId,
                    CreateaAt = DateTime.Now,
                    EarnedHours = 0,
                    BioMachineId = string.Empty,
                    EarnedMinutes = 0,
                    OrganizationId = request.OrganizationId,
                    OvertimeHours = 0,
                    OvertimeMinutes = 0,
                    PolicyId = policyId,
                    Reason = request.Reason,
                    RequestId = requestId,
                    Status = request.Status,
                    StatusUpdateOn = request.StatusUpdateOn,
                    AttendanceStatus = AttendanceStatus.Present,
                    IsNextDay = request.IsNextDay,
                    CardNo = employeeInfo.PunchCode.ToString(),
                    PunchCode = employeeInfo.PunchCode.ToString(),
                };

                attendance = await CalculateEarnedHours(attendance);

                var entity = _context.Attendances.Add(attendance);
                return true;
            }

            return false;
        }

        // Not in use now
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
                        attendance.CheckIn = policy.Policy.ShiftStartTime.ToDatetime(attendance.AttendanceDate);
                        attendance.CheckOut = policy.Policy.ShiftEndTime.ToDatetime(attendance.AttendanceDate, attendance.IsNextDay);
                    }
                }

                attendance = await CalculateEarnedHours(attendance, true);
                var entity = await _context.Attendances.AddAsync(attendance);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        // Not in use now
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

        // Using for manual overtime approval
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
                    AttendanceType = AttendanceType.OverTime,
                    BranchId = request.BranchId,
                    CreateaAt = DateTime.Now,
                    EarnedHours = 0,
                    BioMachineId = string.Empty,
                    EarnedMinutes = 0,
                    OrganizationId = request.OrganizationId,
                    OvertimeHours = 0,
                    OvertimeMinutes = 0,
                    PolicyId = employeeInfo.PolicyId,
                    Reason = request.Reason,
                    RequestId = requestId,
                    Status = request.Status,
                    StatusUpdateOn = request.StatusUpdateOn,
                    OverTimeType = request.OverTimeType,
                    Production = request.Production,
                    RequiredProduction = request.RequiredProduction
                };

                attendance = await CalculateEarnedHours(attendance);

                var entity = _context.Attendances.Add(attendance);
                return true;
            }

            return false;
        }

        private async Task<Attendance> CalculateEarnedHours(Attendance attendance, bool IsUpdate = false)
        {
            // apply policy rules
            var policy = await _orgService.GetPolicyDetailsAsync(attendance.PolicyId);
            if (policy.Policy != null && attendance.CheckOut.HasValue && attendance.CheckIn.HasValue)
            {
                attendance.ExpectedIn = policy.Policy.ShiftStartTime.ToDatetime(attendance.AttendanceDate);
                attendance.ExpectedOut = policy.Policy.ShiftEndTime.ToDatetime(attendance.AttendanceDate, policy.Policy.IsNextDay);

                if (attendance.AttendanceType == AttendanceType.Manual || attendance.AttendanceType == AttendanceType.Bio || attendance.AttendanceType == AttendanceType.System)
                {
                    if (policy.Policy.EarlyArrivalPolicy == EarlyArrivalPolicy.ShiftTime)
                    {
                        var checkInDiff = attendance.ExpectedIn - attendance.CheckIn.Value;
                        if (checkInDiff.TotalMinutes > 0)
                        {
                            attendance.CheckIn = policy.Policy.ShiftStartTime.ToDatetime(attendance.AttendanceDate);
                        }

                        // var checkOutDiff = attendance.ExpectedOut - attendance.CheckOut;
                        // if (checkInDiff.TotalMinutes < 0)
                        // {
                        //     attendance.CheckOut = policy.Policy.ShiftEndTime;
                        // }
                    }

                    var overtimePlan = GetOvertimePlan(attendance.EmployeeId, attendance.AttendanceDate);

                    if (overtimePlan != null)
                    {
                        var expectedOutDiff = attendance.ExpectedOut - attendance.CheckOut.Value;
                        if (expectedOutDiff.TotalMinutes < 0)
                        {
                            var overtimeCheckOutTime = overtimePlan.EndTime.TimeOfDay.ToDatetime(attendance.AttendanceDate, overtimePlan.IsNextDay);

                            var overtimeOutDiff = overtimeCheckOutTime - attendance.CheckOut.Value;
                            if (overtimeOutDiff.TotalMinutes < 0)
                            {
                                attendance.CheckOut = overtimeCheckOutTime;
                            }
                            else
                            {
                                attendance.CheckOut = attendance.CheckOut.Value;
                            }
                        }

                        // calculation if planned overtime exist
                        var overtimeDiffrence = attendance.CheckOut.Value - attendance.ExpectedOut;
                        attendance.OvertimeHours = Math.Round(overtimeDiffrence.TotalHours, 2);
                        attendance.OvertimeMinutes = Math.Round(overtimeDiffrence.TotalMinutes, 2);

                        var timeDiffrence = attendance.ExpectedOut - attendance.CheckIn.Value;
                        attendance.EarnedHours = Math.Round(timeDiffrence.TotalHours, 2);
                        attendance.ActualEarnedHours = attendance.EarnedHours;
                        attendance.EarnedMinutes = timeDiffrence.Minutes;
                    }
                    else
                    {
                        // calculation if not planned overtime exist
                        var timeDiffrence = attendance.CheckOut.Value - attendance.CheckIn.Value;
                        attendance.EarnedHours = Math.Round(timeDiffrence.TotalHours, 2);
                        attendance.ActualEarnedHours = attendance.EarnedHours;
                        attendance.EarnedMinutes = timeDiffrence.Minutes;
                    }
                }
                else if (attendance.AttendanceType == AttendanceType.OverTime)
                {
                    if (attendance.OverTimeType == OverTimeType.Production)
                    {
                        double perHourQty = attendance.RequiredProduction / policy.Policy.DailyWorkingHour;
                        double overtimeHours = attendance.Production / perHourQty;
                        attendance.OvertimeHours = Math.Round(overtimeHours, 2);
                    }
                    else if (attendance.OverTimeType == OverTimeType.Hours)
                    {
                        var overtimeDiffrence = attendance.CheckOut.Value - attendance.CheckIn.Value;
                        attendance.OvertimeHours = Math.Round(overtimeDiffrence.TotalHours, 2);
                    }
                }

                if (attendance.AttendanceType != AttendanceType.Manual && IsOffDay(attendance.AttendanceDate, policy.Policy) && !IsUpdate)
                {
                    attendance.AttendanceStatus = AttendanceStatus.Off;
                }

                if (attendance.AttendanceType != AttendanceType.OverTime)
                {
                    if (policy.Policy.lateComersPenaltyType == LateComersPenaltyType.Hour)
                    {
                        var timeDiff = attendance.CheckIn.Value - attendance.ExpectedIn;

                        if (timeDiff.TotalMinutes > policy.Policy.AllowedLateMinutes)
                        {
                            attendance.IsLateComer = true;
                            attendance.DeductedHours = policy.Policy.lateComersPenalty;
                            attendance.EarnedHours -= attendance.DeductedHours;
                            attendance.LateMinutes = timeDiff.Minutes;
                        }
                    }

                    if (policy.Policy.lateComersPenaltyType == LateComersPenaltyType.Minute)
                    {
                        var timeDiff = attendance.CheckIn.Value - attendance.ExpectedIn;

                        if (timeDiff.TotalMinutes > policy.Policy.AllowedLateMinutes)
                        {
                            attendance.IsLateComer = true;
                            attendance.DeductedHours = policy.Policy.lateComersPenalty / 60;
                            attendance.EarnedHours -= attendance.DeductedHours;
                            attendance.LateMinutes = timeDiff.Minutes;
                        }
                    }

                    if (policy.Policy.EarnedHourPolicy == EarnedHourPolicy.ShiftHour && policy.Policy.DailyWorkingHour > 0 && policy.Policy.DailyWorkingHour < attendance.EarnedHours)
                    {
                        attendance.EarnedHours = policy.Policy.DailyWorkingHour;
                    }
                }
            }

            if (attendance.AttendanceStatus == AttendanceStatus.Absent || attendance.AttendanceStatus == AttendanceStatus.Off)
            {
                attendance.DeductedHours = 0;
                attendance.LateMinutes = 0;
                attendance.OvertimeHours = 0;
                attendance.EarnedHours = 0;
                attendance.ActualEarnedHours = 0;
                attendance.EarnedMinutes = 0;
            }

            attendance.TotalEarnedHours = attendance.EarnedHours;
            return attendance;
        }

        public async Task<List<AttendanceDto>> GetEmployeeAttendance(List<Guid> employeeIds, DateTime startDate, DateTime endDate)
        {
            var entity = await _context.Attendances.AsNoTracking().Where(x => employeeIds.Contains(x.EmployeeId) && x.AttendanceDate.Date >= startDate.Date && x.AttendanceDate.Date <= endDate.Date).ToListAsync();
            return _mapper.Map<List<AttendanceDto>>(entity);
        }

        // Not in use now
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
                    AttendanceType = AttendanceType.Manual,
                    BranchId = request.BranchId,
                    CreateaAt = DateTime.Now,
                    EarnedHours = 0,
                    BioMachineId = string.Empty,
                    EarnedMinutes = 0,
                    OrganizationId = request.OrganizationId,
                    OvertimeHours = 0,
                    OvertimeMinutes = 0,
                    PolicyId = employeeInfo.PolicyId,
                    Reason = request.Reason,
                    Status = request.Status,
                    StatusUpdateOn = request.StatusUpdateOn,
                    AttendanceStatus = AttendanceStatus.Present
                };

                attendance = await CalculateEarnedHours(attendance);

                var entity = _context.Attendances.Add(attendance);
                return true;
            }

            return false;
        }

        private async Task<bool> MarkAutoAbsentOrOffDay(DateTime dateTime)
        {
            List<Shared.DTOs.Dtos.Organizations.PolicyDto> policyList = new List<Shared.DTOs.Dtos.Organizations.PolicyDto>();
            var employeList = await _employeeService.GetEmployeeListAsync();

            foreach (var item in employeList)
            {
                if (_context.Attendances.Any(x => x.EmployeeId == item.Id && x.AttendanceDate.Date == dateTime.Date))
                {
                    continue;
                }

                if (_context.AttendanceLogs.Any(x => x.PunchCode == item.PunchCode.Value.ToString() && x.AttendanceDate.Date == dateTime.Date))
                {
                    continue;
                }

                var policyId = await GetEmployeePolicyId(item.Id.Value);
                if (!policyList.Any(x => x.Id == policyId))
                {
                    var policyDetail = await _orgService.GetPolicyDetailsAsync(policyId);
                    if (policyDetail != null)
                    {
                        policyList.Add(policyDetail.Policy);
                    }
                }

                var policy = policyList.FirstOrDefault(x => x.Id == policyId);

                var status = AttendanceStatus.None;

                if (IsOffDay(dateTime, policy))
                {
                    if (IsSandwich(dateTime, policy, item.Id.Value))
                    {
                        status = AttendanceStatus.Absent;
                    }
                    else
                    {
                        status = AttendanceStatus.Off;
                    }
                }
                else
                {
                    status = AttendanceStatus.Absent;
                }

                var attendance = new Attendance
                {
                    EmployeeId = item.Id.Value,
                    DepartmentId = item.DepartmentId,
                    DesignationId = item.DesignationId,
                    AttendanceDate = dateTime,
                    ActualIn = policy.ShiftStartTime.ToDatetime(dateTime),
                    ActualOut = policy.ShiftEndTime.ToDatetime(dateTime),
                    AddedOn = DateTime.Now,
                    AttendanceType = AttendanceType.System,
                    BranchId = item.BranchId,
                    CreateaAt = DateTime.Now,
                    EarnedHours = 0,
                    BioMachineId = string.Empty,
                    EarnedMinutes = 0,
                    OrganizationId = item.OrganizationId,
                    OvertimeHours = 0,
                    OvertimeMinutes = 0,
                    PolicyId = item.PolicyId,
                    Reason = "Sytem Updated",
                    Status = RequestStatus.Approved,
                    StatusUpdateOn = DateTime.Now,
                    AttendanceStatus = status,
                };

                var entity = _context.Attendances.Add(attendance);
            }

            _context.SaveChanges();
            return false;
        }

        // using in job service
        public bool TiggerAutoAbsentJob(DateTime? datetime)
        {
            // var dt = new DateTime(2022, 08, 31, 00, 00, 00);
            // for (int i = 0; i <= 30; i++)
            // {
            //     dt = dt.AddDays(1);
            //     bool result = MarkAutoAbsentOrOffDay(dt).Result;
            // }

            if (datetime == null)
            {
                datetime = DateTime.Now;
            }
            try
            {
                bool result = MarkAutoAbsentOrOffDay(datetime.Value).Result;
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        // using in job service
        public bool TiggerAutoPresentJob(DateTime? datetime, bool isCheckIn = true)
        {
            if (datetime == null)
            {
                datetime = DateTime.Now;
            }

            TiggerNextDayCheckOutJob(datetime);

            _logger.LogCritical($"Auto Present Job Trigger For : {datetime.Value}");
            try
            {
                var events = _context.AttendanceLogs.AsNoTracking().Where(x => x.AttendanceDate.Date == datetime.Value.Date && !x.IsUsed).OrderBy(x => x.AttendanceDateTime).ToList();
                _logger.LogCritical($"{events.Count} Events Found For : {datetime.Value}");

                // var results = events.GroupBy(
                //    p => p.PunchCode,
                //    (key, g) => new { PunchCode = key, Events = g.ToList() });

                foreach (var item in events)
                {
                    bool result = MarkBioAttendance(item).Result;
                    if (result)
                    {
                        item.IsUsed = true;
                        var entity = _context.AttendanceLogs.Update(item);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                _logger.LogError(ex.ToString());
                throw;
            }

            return true;
        }

        public bool TiggerNextDayCheckOutJob(DateTime? datetime)
        {
            if (datetime == null)
            {
                datetime = DateTime.Now;
            }

            var currentDate = datetime.Value;
            var lastDate = currentDate.AddDays(-1);

            _logger.LogCritical($"Missing Checkout Job Trigger For : {datetime.Value}");
            try
            {
                bool isUpdated = false;
                bool isCheckIn = false;

                var missingCheckOut = _context.Attendances.AsNoTracking().Where(x => x.AttendanceDate.Date == lastDate.Date && x.IsCheckOutMissing && x.IsNextDay).OrderBy(x => x.AttendanceDate).ToList();
                foreach (var item in missingCheckOut)
                {
                    var bioAttendanceLog = _context.AttendanceLogs.AsNoTracking().FirstOrDefault(x => x.PunchCode == item.PunchCode && x.AttendanceDate.Date == currentDate.Date);
                    if (bioAttendanceLog != null)
                    {
                        bool result = MarkBioAttendance(bioAttendanceLog, isCheckIn, item.Id).Result;
                        bioAttendanceLog.IsUsed = true;
                        var entity = _context.AttendanceLogs.Update(bioAttendanceLog);
                        isUpdated = true;
                    }
                }

                if (isUpdated)
                {
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                _logger.LogError(ex.ToString());
                throw;
            }

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

            int absentCount = getSandwichCount(dateTime, policy, employeeId, -1);
            return absentCount == policy.SandwichLeaveCount;
        }

        private int getSandwichCount(DateTime dateTime, Shared.DTOs.Dtos.Organizations.PolicyDto policy, Guid employeeId, int dayCount)
        {
            int absentCount = 0;
            var lastDate = dateTime;
            for (int i = policy.SandwichLeaveCount; i > 0; i--)
            {
                lastDate = lastDate.AddDays(dayCount);
                var attendance = _context.Attendances.FirstOrDefault(x => x.AttendanceDate.Date == lastDate.Date && x.EmployeeId == employeeId);
                if (attendance != null)
                {
                    if (attendance.AttendanceStatus == AttendanceStatus.Absent)
                        absentCount++;
                    else
                        absentCount--;
                }
                else
                {
                    absentCount++;
                }
            }

            return absentCount;
        }

        public async Task<bool> UpdateModification(Guid requestId)
        {
            bool result = false;
            var request = _context.EmployeeRequests.AsNoTracking().Where(x => x.Id == requestId).FirstOrDefault();
            if (request != null)
            {
                if (request.RequestType == RequestType.AttendanceModify)
                {
                    result = await AttendanceModification(request);
                }
                else if (request.RequestType == RequestType.OverTimeModify)
                {
                    result = await OverTimeModification(request);
                }
            }

            return result;
        }

        private async Task<bool> AttendanceModification(EmployeeRequest request)
        {
            var attendance = await _context.Attendances.FirstOrDefaultAsync(x => x.Id == request.ModificationId.Value);
            attendance.AttendanceStatus = request.AttendanceStatus;
            attendance.ActualIn = attendance.CheckIn.HasValue ? attendance.CheckIn.Value : attendance.ActualIn;
            attendance.ActualOut = attendance.CheckOut.HasValue ? attendance.CheckOut.Value : attendance.ActualOut;
            attendance.CheckIn = request.CheckIn;
            attendance.CheckOut = request.CheckOut.Value;
            attendance.Reason = request.Reason;
            attendance = await CalculateEarnedHours(attendance, true);
            var entity = _context.Attendances.Update(attendance);
            return true;
        }

        private async Task<bool> OverTimeModification(EmployeeRequest request)
        {
            var attendance = await _context.Attendances.FirstOrDefaultAsync(x => x.Id == request.ModificationId.Value);
            attendance.AttendanceStatus = request.AttendanceStatus;
            attendance.ActualIn = attendance.CheckIn.Value;
            attendance.ActualOut = attendance.CheckOut.Value;
            attendance.CheckIn = request.CheckIn.Value;
            attendance.CheckOut = request.CheckOut.Value;
            attendance.Reason = request.Reason;
            attendance.Production = request.Production;
            attendance.RequiredProduction = request.RequiredProduction;
            attendance.OverTimeType = request.OverTimeType;
            attendance = await CalculateEarnedHours(attendance, true);
            var entity = _context.Attendances.Update(attendance);
            return true;
        }


        #region Attendance Latest

        private async Task<bool> MarkBioAttendance(BioAttendanceLog bioAttendanceLog)
        {
            int punchCode = int.Parse(bioAttendanceLog.PunchCode);
            DateTime attendanceDate = bioAttendanceLog.AttendanceDateTime;
            var employeeInfo = _context.Employees.FirstOrDefault(x => x.PunchCode == punchCode);
            if (employeeInfo != null)
            {
                // check attendance exist for the date
                var attendance = await _context.Attendances.FirstOrDefaultAsync(x => x.EmployeeId == employeeInfo.Id && x.AttendanceDate.Date == attendanceDate.Date && x.AttendanceType != AttendanceType.OverTime);
                if (attendance != null)
                {
                    if (attendance.AttendanceStatus == AttendanceStatus.Present)
                    {
                        if (attendance.IsCheckOutMissing)
                        {
                            return await CheckoutAttendanceAsync(attendance, bioAttendanceLog);
                        }

                        var extraShift = GetExtraShiftPlan(employeeInfo.Id, attendanceDate.Date);
                        if (extraShift != null)
                        {
                            TimeSpan ts = extraShift.StartTime - attendanceDate;
                            if (ts.TotalMinutes < 30)
                            {
                                return await NewAttendance(bioAttendanceLog, employeeInfo, extraShift);
                            }
                        }

                        _logger.LogCritical($"Attendance Already Exist : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                        return await CheckoutAttendanceAsync(attendance, bioAttendanceLog);
                    }
                    else
                    {
                        attendance.AttendanceStatus = AttendanceStatus.Present;
                        attendance.CheckIn = attendanceDate;
                        attendance.ActualIn = attendanceDate;
                        attendance.IsCheckOutMissing = true;
                        var entity = _context.Attendances.Update(attendance);
                        _context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    _logger.LogCritical($"New Attendance Mark : {punchCode} - {employeeInfo.FullName} - {attendanceDate}");
                    return await NewAttendance(bioAttendanceLog, employeeInfo);
                }
            }
            else
            {
                _logger.LogCritical($"Employee Not Found For Punch Code: {punchCode}");
            }

            return false;
        }

        // mark new attendance
        private async Task<bool> NewAttendance(BioAttendanceLog bioAttendanceLog, Employee employeeInfo, ShiftPlanner shift = null)
        {
            DateTime attendanceDate = bioAttendanceLog.AttendanceDateTime;
            var policyId = await GetEmployeePolicyId(employeeInfo);
            var shiftType = ShiftType.Regular;
            if (shift != null)
            {
                shiftType = ShiftType.Extra;
                policyId = shift.PolicyId;
            }

            var policy = await _orgService.GetPolicyDetailsAsync(policyId);

            var attendance = new Attendance
            {
                EmployeeId = employeeInfo.Id,
                DepartmentId = employeeInfo.DepartmentId,
                DesignationId = employeeInfo.DesignationId,
                AttendanceDate = attendanceDate.Date,
                CheckIn = attendanceDate,
                CheckOut = null,
                ActualIn = attendanceDate,
                AttendanceType = AttendanceType.Bio,
                BranchId = employeeInfo.BranchId,
                CreateaAt = DateTime.Now,
                EarnedHours = 0,
                BioMachineId = string.Empty,
                EarnedMinutes = 0,
                OrganizationId = employeeInfo.OrganizationId,
                OvertimeHours = 0,
                OvertimeMinutes = 0,
                PolicyId = policyId,
                Reason = "Regular Attendance",
                AttendanceStatus = AttendanceStatus.Present,
                IsNextDay = policy != null && policy.Policy != null ? policy.Policy.IsNextDay : false,
                PunchCode = bioAttendanceLog.PunchCode,
                CardNo = bioAttendanceLog.CardNo,
                ShiftType = shiftType,
                IsCheckOutMissing = true,
            };

            var entity = _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return true;
        }

        private async Task<bool> CheckoutAttendanceAsync(Attendance attendance, BioAttendanceLog bioAttendanceLog)
        {
            DateTime attendanceDate = bioAttendanceLog.AttendanceDateTime;
            attendance.CheckOut = attendanceDate;
            attendance.ActualOut = attendanceDate;
            attendance.IsCheckOutMissing = false;

            // TODO: calculate hours logic pending here.
            attendance = await CalculateEarnedHours(attendance);
            var entity = _context.Attendances.Update(attendance);
            _context.SaveChanges();
            return true;
        }

        private ShiftPlanner GetExtraShiftPlan(Guid employeeId, DateTime dateTime)
        {
            return _context.ShiftPlanners.Where(x => x.EmployeeId == employeeId && x.ShiftDate == dateTime.Date).FirstOrDefault();
        }

        private OvertimeRequest GetOvertimePlan(Guid employeeId, DateTime dateTime)
        {
            return _context.OvertimeRequests.Where(x => x.EmployeeId == employeeId && x.OvertimeDate == dateTime.Date).FirstOrDefault();
        }

        public async Task<bool> DeleteAttendanceOrOvertime(Guid requestId)
        {
            bool result = false;
            var request = _context.EmployeeRequests.AsNoTracking().Where(x => x.Id == requestId).FirstOrDefault();
            if (request != null)
            {
                var attendance = await _context.Attendances.FirstOrDefaultAsync(x => x.Id == request.ModificationId.Value);
                if (attendance != null)
                {
                    var entity = _context.Attendances.Remove(attendance);
                    return true;
                }
            }

            return result;
        }
        #endregion
    }
}