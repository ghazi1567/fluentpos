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
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.EmployeeRequests;
using FluentPOS.Shared.DTOs.People.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    internal class EmployeeRequestQueryHandler :
        IRequestHandler<GetEmployeeRequestsQuery, PaginatedResult<EmployeeRequestDto>>,
        IRequestHandler<GetMyQueueQuery, PaginatedResult<EmployeeRequestDto>>,
        IRequestHandler<GetEmployeeRequestByIdQuery, Result<GetEmployeeRequestByIdResponse>>
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CustomerQueryHandler> _localizer;

        public EmployeeRequestQueryHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IStringLocalizer<CustomerQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<EmployeeRequestDto>> Handle(GetEmployeeRequestsQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            Expression<Func<EmployeeRequest, GetEmployeeRequestsResponse>> expression = e => new GetEmployeeRequestsResponse(e.Id, e.CreateaAt, e.UpdatedAt, e.OrganizationId, e.BranchId, Guid.Empty, e.EmployeeId, e.DepartmentId, e.PolicyId, e.DesignationId, e.RequestType, e.RequestedOn, e.RequestedBy, e.AttendanceDate, e.CheckIn, e.CheckOut, e.OvertimeHours, e.OverTimeType, e.Reason);

            var queryable = _context.EmployeeRequests.Where(x => x.EmployeeId == request.EmployeeId || x.RequestedBy == request.EmployeeId).AsNoTracking().OrderBy(x => x.Id).AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(c => c.Reason.Contains(request.SearchString));
            }

            if (request.RequestType != null)
            {
                queryable = queryable.Where(c => c.RequestType == request.RequestType.Value);
            }

            if (request.OrganizationId.HasValue)
            {
                queryable = queryable.Where(x => x.OrganizationId == request.OrganizationId.Value);
            }

            if (request.BranchId.HasValue)
            {
                queryable = queryable.Where(x => x.BranchId == request.BranchId.Value);
            }

            // var customerList = await queryable
            //    .AsNoTracking()
            //    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            var requests = await (from r in queryable
                                         join e in _context.Employees
                                         on r.EmployeeId equals e.Id
                                         join b in _context.Employees
                                         on r.RequestedBy equals b.Id
                                         select new EmployeeRequestDto
                                         {
                                             Id = r.Id,
                                             BranchId = r.BranchId,
                                             CreateaAt = r.CreateaAt,
                                             OrganizationId = r.OrganizationId,
                                             Status = r.Status,
                                             StatusUpdateOn = r.StatusUpdateOn,
                                             UpdatedAt = r.UpdatedAt,
                                             EmployeeId = r.EmployeeId,
                                             AssignedOn = r.AssignedOn,
                                             AssignedTo = r.AssignedTo,
                                             AttendanceDate = r.AttendanceDate,
                                             CheckIn = r.CheckIn,
                                             CheckOut = r.CheckOut,
                                             DepartmentId = r.DepartmentId,
                                             DesignationId = r.DesignationId,
                                             WorkflowId = r.WorkflowId,
                                             OvertimeHours = r.OvertimeHours,
                                             OverTimeType = r.OverTimeType,
                                             PolicyId = r.PolicyId,
                                             Reason = r.Reason,
                                             RequestedBy = r.RequestedBy,
                                             RequestedOn = r.RequestedOn,
                                             RequestType = r.RequestType,
                                             RequestedForName = e.FullName,
                                             RequestedByName = b.FullName
                                         })
                               .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (requests == null)
            {
                throw new PeopleException(_localizer["Request Not Found!"], HttpStatusCode.NotFound);
            }

            // return _mapper.Map<PaginatedResult<EmployeeRequestDto>>(customerList);
            return requests;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<GetEmployeeRequestByIdResponse>> Handle(GetEmployeeRequestByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var employee = await _context.EmployeeRequests.Where(c => c.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (employee == null)
            {
                throw new PeopleException(_localizer["Request Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedEmployee = _mapper.Map<GetEmployeeRequestByIdResponse>(employee);
            return await Result<GetEmployeeRequestByIdResponse>.SuccessAsync(mappedEmployee);
        }

        public async Task<PaginatedResult<EmployeeRequestDto>> Handle(GetMyQueueQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<EmployeeRequest, GetEmployeeRequestsResponse>> expression = e => new GetEmployeeRequestsResponse(e.Id, e.CreateaAt, e.UpdatedAt, e.OrganizationId, e.BranchId, Guid.Empty, e.EmployeeId, e.DepartmentId, e.PolicyId, e.DesignationId, e.RequestType, e.RequestedOn, e.RequestedBy, e.AttendanceDate, e.CheckIn, e.CheckOut, e.OvertimeHours, e.OverTimeType, e.Reason);

            var queryable = _context.EmployeeRequests.Where(x => x.AssignedTo == request.EmployeeId && (x.Status == Shared.DTOs.Enums.RequestStatus.Pending || x.Status == Shared.DTOs.Enums.RequestStatus.InProgress)).AsNoTracking().OrderBy(x => x.Id).AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(c => c.Reason.Contains(request.SearchString));
            }

            if (request.RequestType != null)
            {
                if (request.RequestType == Shared.DTOs.Enums.RequestType.Attendance)
                {
                    queryable = queryable.Where(c => c.RequestType == Shared.DTOs.Enums.RequestType.Attendance || c.RequestType == Shared.DTOs.Enums.RequestType.AttendanceModify);
                }
                if (request.RequestType == Shared.DTOs.Enums.RequestType.OverTime)
                {
                    queryable = queryable.Where(c => c.RequestType == Shared.DTOs.Enums.RequestType.OverTime || c.RequestType == Shared.DTOs.Enums.RequestType.OverTimeModify);
                }
            }

            if (request.OrganizationId.HasValue)
            {
                queryable = queryable.Where(x => x.OrganizationId == request.OrganizationId.Value);
            }

            if (request.BranchId.HasValue)
            {
                queryable = queryable.Where(x => x.BranchId == request.BranchId.Value);
            }

            var myQueueRequests = await (from r in queryable
                                join e in _context.Employees
                                on r.EmployeeId equals e.Id
                                join b in _context.Employees
                                on r.RequestedBy equals b.Id
                                select new EmployeeRequestDto
                                {
                                    Id = r.Id,
                                    BranchId = r.BranchId,
                                    CreateaAt = r.CreateaAt,
                                    OrganizationId = r.OrganizationId,
                                    Status = r.Status,
                                    StatusUpdateOn = r.StatusUpdateOn,
                                    UpdatedAt = r.UpdatedAt,
                                    EmployeeId = r.EmployeeId,
                                    AssignedOn = r.AssignedOn,
                                    Approvals = new List<RequestApprovalDto>(),
                                    AssignedTo = r.AssignedTo,
                                    AttendanceDate = r.AttendanceDate,
                                    CheckIn = r.CheckIn,
                                    CheckOut = r.CheckOut,
                                    DepartmentId = r.DepartmentId,
                                    DesignationId = r.DesignationId,
                                    WorkflowId = r.WorkflowId,
                                    OvertimeHours = r.OvertimeHours,
                                    OverTimeType = r.OverTimeType,
                                    PolicyId = r.PolicyId,
                                    Reason = r.Reason,
                                    RequestedBy = r.RequestedBy,
                                    RequestedOn = r.RequestedOn,
                                    RequestType = r.RequestType,
                                    RequestedForName = e.FullName,
                                    RequestedByName = b.FullName,
                                    AttendanceStatus =r.AttendanceStatus,
                                    ModificationId = r.ModificationId,
                                    Production =r.Production,
                                    RequiredProduction =r.RequiredProduction
                                })
                                .ToPaginatedListAsync(request.PageNumber, request.PageSize);


            if (myQueueRequests == null)
            {
                throw new PeopleException(_localizer["Request Not Found!"], HttpStatusCode.NotFound);
            }

            // return _mapper.Map<PaginatedResult<EmployeeRequestDto>>(myQueueRequests);
            return myQueueRequests;
        }

        public async Task<List<RequestApprovalDto>> Handle(GetRequestApproverListQuery request, CancellationToken cancellationToken)
        {
            var employeeRequest = await _context.EmployeeRequests.Where(c => c.Id == request.RequestId).FirstOrDefaultAsync(cancellationToken);
            if (employeeRequest == null)
            {
                throw new PeopleException(_localizer["Request Not Found!"], HttpStatusCode.NotFound);
            }

            var approvalDtos = (from r in _context.RequestApprovals
                                join e in _context.Employees
                                on r.ApproverId equals e.Id
                                where r.EmployeeRequestId == employeeRequest.Id
                                select new RequestApprovalDto
                                {
                                    Id = e.Id,
                                    EmployeeRequestId = employeeRequest.Id,
                                    ApproverId = r.ApproverId,
                                    ApprovalIndex = r.ApprovalIndex,
                                    BranchId = r.BranchId,
                                    Comments = r.Comments,
                                    CreateaAt = r.CreateaAt,
                                    EmployeeName = e.FullName,
                                    IpAddress = string.Empty,
                                    OrganizationId = r.OrganizationId,
                                    Status = r.Status,
                                    StatusUpdateOn = r.StatusUpdateOn,
                                    UpdatedAt = r.UpdatedAt,
                                    UserId = Guid.Empty,
                                }).ToList();

            return approvalDtos;
        }

    }
}