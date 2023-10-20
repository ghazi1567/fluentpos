// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Dtos;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Interfaces.Services.Organization;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    internal class ShiftPlanningQueryHandler :
        IRequestHandler<GetShiftPlanningQuery, PaginatedResult<ShiftPlannerDto>>
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AttendanceQueryHandler> _localizer;
        private readonly IEmployeeService _employeeService;
        private readonly IOrgService _orgService;

        public ShiftPlanningQueryHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IStringLocalizer<AttendanceQueryHandler> localizer,
            IEmployeeService employeeService,
            IOrgService orgService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _employeeService = employeeService;
            _orgService = orgService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<ShiftPlannerDto>> Handle(GetShiftPlanningQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var queryable = _context.ShiftPlanners.AsNoTracking().OrderByDescending(x => x.ShiftDate).AsQueryable();

            // string ordering = new OrderByConverter().Convert(request.OrderBy);
            string ordering = request.OrderBy;

            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderByDescending(a => a.ShiftDate);

            var shiftPlannersList = await queryable
               .AsNoTracking()
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (shiftPlannersList == null)
            {
                throw new PeopleException(_localizer["Request Not Found!"], HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<PaginatedResult<ShiftPlannerDto>>(shiftPlannersList);

            var myEmpIds = response.Data.Select(x => x.EmployeeId).ToList();
            var myEmployees = await _employeeService.GetEmployeeDetailsAsync(myEmpIds);
            var policies = await _orgService.GetAllPoliciesAsync();

            foreach (var item in response.Data)
            {
                item.EmployeeName = myEmployees.FirstOrDefault(x => x.Id == item.EmployeeId)?.FullName;
                item.PunchCode = item.PunchCode == null ? myEmployees.FirstOrDefault(x => x.Id == item.EmployeeId)?.PunchCode : item.PunchCode;
                item.PolicyName = policies.FirstOrDefault(x => x.Id == item.PolicyId)?.Name;
                item.StartTimeSpan = item.StartTime.TimeOfDay;
                item.EndTimeSpan = item.EndTime.TimeOfDay;
            }

            return response;
        }

    }
}