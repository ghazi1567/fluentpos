// --------------------------------------------------------------------------------------------------
// <copyright file="CustomerQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    internal class EmployeeQueryHandler :
        IRequestHandler<GetEmployeesQuery, PaginatedResult<GetEmployeesResponse>>,
        IRequestHandler<GetEmployeeByIdQuery, Result<GetEmployeeByIdResponse>>,
        IRequestHandler<GetEmployeeImageQuery, Result<string>>
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CustomerQueryHandler> _localizer;

        public EmployeeQueryHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IStringLocalizer<CustomerQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<GetEmployeesResponse>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            Expression<Func<Employee, GetEmployeesResponse>> expression = e => new GetEmployeesResponse(e.Id, e.CreateaAt, e.UpdatedAt, e.OrganizationId, e.BranchId, Guid.Empty, String.Empty, e.Prefix, e.FirstName, e.LastName, e.FullName, e.FatherName, e.EmployeeCode, e.PunchCode, e.MobileNo, e.PhoneNo, e.Email, e.AllowManualAttendance, e.UserName, e.Password, e.MaritalStatus, e.Gender, e.DateOfBirth, e.PlaceOfBirth, e.FamilyCode, e.Religion, e.BankAccountNo, e.BankAccountTitle, e.BankName, e.BankBranchCode, e.Address, e.CnicNo, e.CnicIssueDate, e.CnicExpireDate, e.DepartmentId, e.DesignationId, e.PolicyId, e.EmployeeStatus, e.JoiningDate, e.ConfirmationDate, e.ContractStartDate, e.ContractEndDate, e.ResignDate, e.ImageUrl,e.Active,e.BasicSalary,e.ReportingTo,e.PaymentMode);

            var queryable = _context.Employees.AsNoTracking().OrderBy(x => x.Id).AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(c => c.FullName.Contains(request.SearchString) || c.PhoneNo.Contains(request.SearchString) || c.Email.Contains(request.SearchString));
            }

            if (request.OrganizationId.HasValue)
            {
                queryable = queryable.Where(x => x.OrganizationId == request.OrganizationId.Value);
            }

            if (request.BranchId.HasValue)
            {
                queryable = queryable.Where(x => x.BranchId == request.BranchId.Value);
            }

            var customerList = await queryable
                .Select(expression)
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (customerList == null)
            {
                throw new PeopleException(_localizer["Customers Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetEmployeesResponse>>(customerList);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<GetEmployeeByIdResponse>> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var employee = await _context.Employees.Where(c => c.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (employee == null)
            {
                throw new PeopleException(_localizer["Customer Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedEmployee = _mapper.Map<GetEmployeeByIdResponse>(employee);
            return await Result<GetEmployeeByIdResponse>.SuccessAsync(mappedEmployee);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<string>> Handle(GetEmployeeImageQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            string data = await _context.Customers.AsNoTracking()
                .Where(c => c.Id == request.Id)
                .Select(a => a.ImageUrl)
                .FirstOrDefaultAsync(cancellationToken);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}