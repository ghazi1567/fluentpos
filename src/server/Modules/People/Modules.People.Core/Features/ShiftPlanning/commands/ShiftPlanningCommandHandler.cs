// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
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

namespace FluentPOS.Modules.People.Core.Features.Employees.Commands
{
    internal class ShiftPlanningCommandHandler :
        IRequestHandler<RemoveShiftPlanningCommand, Result<Guid>>,
        IRequestHandler<RegisterShiftPlanningCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<EmployeeCommandHandler> _localizer;
        private readonly IAttendanceService _attendanceService;
        private readonly IPayrollService _payrollService;

        private readonly IWorkFlowService _workFlowService;

        public ShiftPlanningCommandHandler(
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
        public async Task<Result<Guid>> Handle(RemoveShiftPlanningCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var shiftPlanner = await _context.ShiftPlanners.Where(c => c.UUID == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (shiftPlanner != null)
            {
                _context.ShiftPlanners.Remove(shiftPlanner);
                _context.SaveChanges();
                return await Result<Guid>.SuccessAsync(Guid.NewGuid(), _localizer["Shift saved!!"]);
            }
            else
            {
                throw new PeopleException(_localizer[$"Shift not found."], HttpStatusCode.Ambiguous);
            }
        }

        public async Task<Result<Guid>> Handle(RegisterShiftPlanningCommand command, CancellationToken cancellationToken)
        {
            if (command.EmployeeIds.Count == 0)
            {
                throw new PeopleException(_localizer[$"Please select employee."], HttpStatusCode.Ambiguous);
            }

            List<ShiftPlanner> shiftPlannerList = new List<ShiftPlanner>();
            foreach (var item in command.EmployeeIds)
            {
                shiftPlannerList.Add(new ShiftPlanner
                {
                    EmployeeId = item,
                    PolicyId = command.PolicyId,
                    ShiftDate = command.ShiftDate.Date,
                    StartTime = command.StartTimeSpan.ToDatetime(command.ShiftDate.Date),
                    EndTime = command.EndTimeSpan.ToDatetime(command.ShiftDate.Date,command.IsNextDay),
                    IsNextDay = command.IsNextDay
                });
            }

            await _context.ShiftPlanners.AddRangeAsync(shiftPlannerList);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(Guid.NewGuid(), _localizer["Shift saved!!"]);
        }
    }
}