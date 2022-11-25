// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Dtos;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    internal class DashboardQueryHandler :
        IRequestHandler<GetDashboardQuery, DashboardDto>
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AttendanceQueryHandler> _localizer;
        private readonly IEmployeeService _employeeService;

        public DashboardQueryHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IStringLocalizer<AttendanceQueryHandler> localizer,
            IEmployeeService employeeService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _employeeService = employeeService;
        }

        public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var dto = new DashboardDto();
            var totalEmployees = await _employeeService.GetEmployeeCountAsync();
            var attendance = _context.Attendances.Where(x => x.AttendanceDate.Date == request.StartDate.Date).ToList();

            var endDate = request.StartDate;
            var startDate = request.StartDate.AddDays(-7);

            var last7DaysAttendance = _context.Attendances.Where(x => x.AttendanceDate.Date >= startDate.Date && x.AttendanceDate.Date <= endDate.Date).ToList();

            dto.TotalEmployees = totalEmployees;
            dto.Presents = attendance.Count(x => x.AttendanceStatus == Shared.DTOs.Enums.AttendanceStatus.Present);
            dto.Absents = attendance.Count(x => x.AttendanceStatus == Shared.DTOs.Enums.AttendanceStatus.Absent);
            dto.LateComer = attendance.Count(x => x.AttendanceStatus == Shared.DTOs.Enums.AttendanceStatus.Present && x.IsLateComer);
            dto.Absents = attendance.Count(x => x.AttendanceStatus == Shared.DTOs.Enums.AttendanceStatus.Absent);
            dto.Last7DaysAbsents = last7DaysAttendance.Count(x => x.AttendanceStatus == Shared.DTOs.Enums.AttendanceStatus.Absent);
            dto.Last7DaysLateComer = last7DaysAttendance.Count(x => x.AttendanceStatus == Shared.DTOs.Enums.AttendanceStatus.Present && x.IsLateComer);

            return dto;
        }
    }
}