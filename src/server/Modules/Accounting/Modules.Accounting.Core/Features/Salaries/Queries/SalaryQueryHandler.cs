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
using FluentPOS.Modules.Accounting.Core.Abstractions;
using FluentPOS.Modules.Accounting.Core.Dtos;
using FluentPOS.Modules.Accounting.Core.Exceptions;
using FluentPOS.Modules.People.Core.Features.Customers.Queries;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Salaries.Queries
{
    internal class SalaryQueryHandler :
        IRequestHandler<GetSalaryQuery, PaginatedResult<SalaryDto>>,
        IRequestHandler<GetSalaryByIdQuery, Result<SalaryDto>>
    {
        private readonly IAccountingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalaryQueryHandler> _localizer;
        private readonly IEmployeeService _employeeService;

        public SalaryQueryHandler(
            IAccountingDbContext context,
            IMapper mapper,
            IStringLocalizer<SalaryQueryHandler> localizer,
            IEmployeeService employeeService)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _employeeService = employeeService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<SalaryDto>> Handle(GetSalaryQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var queryable = _context.Salaries.AsNoTracking().OrderBy(x => x.Id).AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (request.OrganizationId.HasValue)
            {
                queryable = queryable.Where(x => x.OrganizationId == request.OrganizationId.Value);
            }

            if (request.BranchId.HasValue)
            {
                queryable = queryable.Where(x => x.BranchId == request.BranchId.Value);
            }

            var result = await queryable
               .AsNoTracking()
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);


            if (result == null)
            {
                throw new AccountingException(_localizer["Salary Not Found!"], HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<PaginatedResult<SalaryDto>>(result);
            var ids = response.Data.Select(x => x.EmployeeId).Distinct().ToList();
            var employeeDetails = await _employeeService.GetEmployeeDetailsAsync(ids);

            foreach (var item in response.Data)
            {
                item.EmployeeName = employeeDetails.Any(x => x.Id == item.EmployeeId) ? employeeDetails.FirstOrDefault(x => x.Id == item.EmployeeId).FullName : string.Empty;
            }

            return response;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<SalaryDto>> Handle(GetSalaryByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var employee = await _context.Salaries.Where(c => c.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (employee == null)
            {
                throw new AccountingException(_localizer["Salary Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedEmployee = _mapper.Map<SalaryDto>(employee);
            return await Result<SalaryDto>.SuccessAsync(mappedEmployee);
        }

    }
}