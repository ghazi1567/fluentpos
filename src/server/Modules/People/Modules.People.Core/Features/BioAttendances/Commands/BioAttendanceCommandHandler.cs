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
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Employees.Commands
{
    internal class BioAttendanceCommandHandler :
        IRequestHandler<RegisterBioAttendanceCommand, Result<Guid>>,
        IRequestHandler<ImportBioAttendanceCommand, Result<Guid>>,
        IRequestHandler<UpdateBioAttendanceCommand, Result<Guid>>

    {
        private readonly IDistributedCache _cache;
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<EmployeeCommandHandler> _localizer;
        private readonly IAttendanceService _attendanceService;
        private readonly IWorkFlowService _workFlowService;

        public BioAttendanceCommandHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<EmployeeCommandHandler> localizer,
            IDistributedCache cache,
            IEmployeeService employeeService,
            IWorkFlowService workFlowService,
            IAttendanceService attendanceService)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
            _attendanceService = attendanceService;
            _workFlowService = workFlowService;
        }

        public async Task<Result<Guid>> Handle(RegisterBioAttendanceCommand command, CancellationToken cancellationToken)
        {
            var attendanceLog = new BioAttendanceLog();
            attendanceLog = _mapper.Map<BioAttendanceLog>(command);

            if (!string.IsNullOrEmpty(command.PunchCode))
            {
                bool attendance = await _attendanceService.MarkBioAttendance(int.Parse(command.PunchCode), command.AttendanceDateTime);
            }

            await _context.AttendanceLogs.AddAsync(attendanceLog, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(attendanceLog.Id, _localizer["Attendance Log Saved"]);
        }

        public async Task<Result<Guid>> Handle(ImportBioAttendanceCommand command, CancellationToken cancellationToken)
        {
            if (command.AttendanceLogs.Count == 0)
            {
                throw new PeopleException(_localizer["Attendance Log Not Found!"], HttpStatusCode.NotFound);
            }

            var attendanceLog = _mapper.Map<List<BioAttendanceLog>>(command.AttendanceLogs);

            await _context.AttendanceLogs.AddRangeAsync(attendanceLog, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(attendanceLog[0].Id, _localizer["Attendance Log Saved"]);
        }

        public async Task<Result<Guid>> Handle(UpdateBioAttendanceCommand command, CancellationToken cancellationToken)
        {
            var attendance = await _context.Attendances.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (attendance != null)
            {
                _context.Attendances.Update(attendance);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(attendance.Id, _localizer["Attendance Log Updated"]);
            }
            else
            {
                throw new PeopleException(_localizer["Attendance Log Not Found!"], HttpStatusCode.NotFound);
            }
        }
    }
}