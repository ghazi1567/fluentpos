// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Interfaces.Services.Accounting;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Features.Employees.Commands
{
    internal class EmployeeRequestCommandHandler :
        IRequestHandler<RegisterEmployeeRequestCommand, Result<Guid>>,
        IRequestHandler<RemoveEmployeeRequestCommand, Result<Guid>>,
        IRequestHandler<UpdateEmployeeRequestCommand, Result<Guid>>,
        IRequestHandler<UpdateApprovalsCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<EmployeeCommandHandler> _localizer;
        private readonly IEmployeeService _employeeService;
        private readonly IWorkFlowService _workFlowService;
        private readonly IAttendanceService _attendanceService;
        private readonly IPayrollService _payrollService;

        public EmployeeRequestCommandHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<EmployeeCommandHandler> localizer,
            IDistributedCache cache,
            IEmployeeService employeeService,
            IWorkFlowService workFlowService,
            IAttendanceService attendanceService,
            IPayrollService payrollService)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
            _employeeService = employeeService;
            _workFlowService = workFlowService;
            _attendanceService = attendanceService;
            _payrollService = payrollService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterEmployeeRequestCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            bool isPayrollGenerated = await _payrollService.IsPayrollGenerated(command.EmployeeId, command.AttendanceDate.Date);
            if (isPayrollGenerated)
            {
                throw new PeopleException(_localizer[$"Payroll Already Generated For Month : {command.AttendanceDate.ToString("MMMM")}!"], HttpStatusCode.Ambiguous);
            }

            if (command.RequestType == Shared.DTOs.Enums.RequestType.OverTime)
            {
                bool attendance = await _attendanceService.IsOverTimeExist(command.EmployeeId, command.AttendanceDate.Date);
                if (attendance)
                {
                    throw new PeopleException(_localizer[$"OverTime Already Marked For Date : {command.AttendanceDate}!"], HttpStatusCode.Ambiguous);
                }
            }
            else if (command.RequestType == Shared.DTOs.Enums.RequestType.Attendance)
            {
                bool attendance = await _attendanceService.IsAttendanceExist(command.EmployeeId, command.AttendanceDate.Date);
                if (attendance)
                {
                    throw new PeopleException(_localizer[$"Attendance Already Marked For Date : {command.AttendanceDate}!"], HttpStatusCode.Ambiguous);
                }
            }

            var employeeRequest = _mapper.Map<EmployeeRequest>(command);

            employeeRequest.CheckIn = command.CheckInTime.ToDatetime(command.AttendanceDate);
            employeeRequest.CheckOut = command.CheckOutTime.ToDatetime(command.AttendanceDate, command.IsNextDay);

            var employeeDetails = await _employeeService.GetEmployeeDetailsAsync(employeeRequest.EmployeeId);
            if (employeeDetails != null)
            {
                employeeRequest.DepartmentId = employeeDetails.DepartmentId;
                employeeRequest.PolicyId = employeeDetails.PolicyId;
                employeeRequest.DesignationId = employeeDetails.DesignationId;
            }

            if (command.RequestType == Shared.DTOs.Enums.RequestType.OverTime && command.OverTimeType == Shared.DTOs.Enums.OverTimeType.Production)
            {
                var perHourQty = command.RequiredProduction / 8;
                var overtimeHours = command.Production / Math.Round(perHourQty, 2);
                DateTime checkIn = new DateTime(command.AttendanceDate.Year, command.AttendanceDate.Month, command.AttendanceDate.Day, 00, 00, 01);
                DateTime checkOut = checkIn.AddHours(overtimeHours);
                employeeRequest.CheckIn = checkIn;
                employeeRequest.CheckOut = checkOut;
                employeeRequest.OvertimeHours = Math.Round(overtimeHours, 2);
            }

            await _context.EmployeeRequests.AddAsync(employeeRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _workFlowService.AssignAprroversAsync(employeeRequest.UUID);
            return await Result<Guid>.SuccessAsync(employeeRequest.UUID, _localizer["Request Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateEmployeeRequestCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            bool isPayrollGenerated = await _payrollService.IsPayrollGenerated(command.EmployeeId, command.AttendanceDate.Date);
            if (isPayrollGenerated)
            {
                throw new PeopleException(_localizer[$"Payroll Already Generated For Month : {command.AttendanceDate.ToString("MMMM")}!"], HttpStatusCode.Ambiguous);
            }

            var employeeRequest = await _context.EmployeeRequests.Where(c => c.UUID == command.UUID).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (employeeRequest != null)
            {
                employeeRequest = _mapper.Map<EmployeeRequest>(command);

                _context.EmployeeRequests.Update(employeeRequest);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(employeeRequest.UUID, _localizer["Request Updated"]);
            }
            else
            {
                throw new PeopleException(_localizer["Request Not Found!"], HttpStatusCode.NotFound);
            }
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveEmployeeRequestCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var employeeRequest = await _context.EmployeeRequests.Where(c => c.UUID == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (employeeRequest != null)
            {
                bool isPayrollGenerated = await _payrollService.IsPayrollGenerated(employeeRequest.AttendanceDate.Date);
                if (isPayrollGenerated)
                {
                    throw new PeopleException(_localizer[$"Payroll Already Generated For Month : {employeeRequest.AttendanceDate.ToString("MMMM")}!"], HttpStatusCode.Ambiguous);
                }
                _context.EmployeeRequests.Remove(employeeRequest);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(employeeRequest.UUID, _localizer["Request Deleted"]);
            }
            else
            {
                throw new PeopleException(_localizer["Request Not Found!"], HttpStatusCode.NotFound);
            }
        }

        public async Task<Result<Guid>> Handle(UpdateApprovalsCommand command, CancellationToken cancellationToken)
        {
            if (command.Status == Shared.DTOs.Enums.RequestStatus.Approved)
            {
                var employeeRequest = await _context.EmployeeRequests.Where(c => c.UUID == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
                if (employeeRequest != null)
                {
                    bool isPayrollGenerated = await _payrollService.IsPayrollGenerated(employeeRequest.EmployeeId, employeeRequest.AttendanceDate.Date);
                    if (isPayrollGenerated)
                    {
                        throw new PeopleException(_localizer[$"Payroll Already Generated For Month : {employeeRequest.AttendanceDate.ToString("MMMM")}!"], HttpStatusCode.Ambiguous);
                    }
                }

                bool result = await _workFlowService.ApproveRequestAsync(command.Id, command.ApproverId, command.Comments);
                if (result)
                {
                    return await Result<Guid>.SuccessAsync(command.Id, _localizer["Request Approved!"]);
                }
                else
                {
                    throw new PeopleException(_localizer["Unable to approve request!"], HttpStatusCode.NotFound);
                }
            }
            else
            {
                bool result = await _workFlowService.RejectRequestAsync(command.Id, command.ApproverId, command.Comments);
                if (result)
                {
                    return await Result<Guid>.SuccessAsync(command.Id, _localizer["Request Reject!"]);
                }
                else
                {
                    throw new PeopleException(_localizer["Unable to reject request!"], HttpStatusCode.NotFound);
                }
            }
        }

    }
}