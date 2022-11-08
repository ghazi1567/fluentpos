// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Interfaces.Services.Accounting;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Employees.Commands
{
    internal class AttendanceCommandHandler :
        IRequestHandler<UpdateAttendanceCommand, Result<Guid>>,
        IRequestHandler<RegisterAttendanceCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<EmployeeCommandHandler> _localizer;
        private readonly IAttendanceService _attendanceService;
        private readonly IPayrollService _payrollService;

        private readonly IWorkFlowService _workFlowService;

        public AttendanceCommandHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<EmployeeCommandHandler> localizer,
            IDistributedCache cache,
            IAttendanceService attendanceService,
            IWorkFlowService workFlowService,
            IPayrollService payrollService)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
            _attendanceService = attendanceService;
            _workFlowService = workFlowService;
            _payrollService = payrollService;
        }


#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateAttendanceCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            bool isPayrollGenerated = await _payrollService.IsPayrollGenerated(command.EmployeeId, command.AttendanceDate.Date);
            if (isPayrollGenerated)
            {
                throw new PeopleException(_localizer[$"Payroll Already Generated For Month : {command.AttendanceDate.ToString("MMMM")}!"], HttpStatusCode.Ambiguous);
            }

            bool result = await _attendanceService.UpdateAttendance(command);
            if (result)
            {
                return await Result<Guid>.SuccessAsync(command.Id.Value, _localizer["Request Updated"]);
            }
            else
            {
                throw new PeopleException(_localizer["Request Not Found!"], HttpStatusCode.NotFound);
            }
        }

        public async Task<Result<Guid>> Handle(RegisterAttendanceCommand command, CancellationToken cancellationToken)
        {
            bool isPayrollGenerated = await _payrollService.IsPayrollGenerated(command.EmployeeId, command.AttendanceDate.Date);
            if (isPayrollGenerated)
            {
                throw new PeopleException(_localizer[$"Payroll Already Generated For Month : {command.AttendanceDate.ToString("MMMM")}!"], HttpStatusCode.Ambiguous);
            }

            bool attendance = await _attendanceService.IsAttendanceExist(command.EmployeeId, command.AttendanceDate.Date);
            if (attendance == false)
            {
                bool result = await _attendanceService.MarkNewAttendance(command.EmployeeId, command.AttendanceDate, command.AttendanceStatus);
                if (result)
                {
                    return await Result<Guid>.SuccessAsync(Guid.NewGuid(), _localizer["Request Updated"]);
                }
                else
                {
                    throw new PeopleException(_localizer["Request Not Found!"], HttpStatusCode.NotFound);
                }
            }
            else
            {
                throw new PeopleException(_localizer[$"Attendance Already Marked For Date : {command.AttendanceDate}!"], HttpStatusCode.Ambiguous);
            }
        }
    }
}