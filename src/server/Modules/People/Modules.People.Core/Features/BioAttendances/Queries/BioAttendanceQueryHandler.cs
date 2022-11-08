// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Dtos;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.EmployeeRequests;
using FluentPOS.Shared.DTOs.People.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    internal class BioAttendanceQueryHandler :
        IRequestHandler<GetBioAttendanceQuery, PaginatedResult<AttendanceDto>>
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CustomerQueryHandler> _localizer;
        private readonly IEmployeeService _employeeService;

        public BioAttendanceQueryHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IStringLocalizer<CustomerQueryHandler> localizer,
            IEmployeeService employeeService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _employeeService = employeeService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<AttendanceDto>> Handle(GetBioAttendanceQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {

            var myEmployees = await _employeeService.GetMyReporterEmployeeListAsync(request.EmployeeId, true);
            var myEmpIds = myEmployees.Select(x => x.Id.Value).ToList();

            Expression<Func<EmployeeRequest, GetEmployeeRequestsResponse>> expression = e => new GetEmployeeRequestsResponse(e.Id, e.CreateaAt, e.UpdatedAt, e.OrganizationId, e.BranchId, Guid.Empty, e.EmployeeId, e.DepartmentId, e.PolicyId, e.DesignationId, e.RequestType, e.RequestedOn, e.RequestedBy, e.AttendanceDate, e.CheckIn, e.CheckOut, e.OvertimeHours, e.OverTimeType, e.Reason);

            var queryable = _context.Attendances.Where(x => myEmpIds.Contains(x.EmployeeId)).AsNoTracking().OrderByDescending(x => x.AttendanceDate).AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderByDescending(a => a.AttendanceDate);

            if (request.AttendanceStatus != null)
            {
                queryable = queryable.Where(c => c.AttendanceStatus == request.AttendanceStatus);
            }

            var attendanceList = await queryable
               .AsNoTracking()
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (attendanceList == null)
            {
                throw new PeopleException(_localizer["Request Not Found!"], HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<PaginatedResult<AttendanceDto>>(attendanceList);

            foreach (var item in response.Data)
            {
                item.EmployeeName = myEmployees.FirstOrDefault(x => x.Id == item.EmployeeId)?.FullName;
            }

            return response;
        }
    }
}