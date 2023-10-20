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
    internal class PayslipQueryHandler :
        IRequestHandler<GetPayslipRequestQuery, PaginatedResult<PayrollDto>>,
        IRequestHandler<GetPayslipRequestByIdQuery, Result<PayrollDto>>
    {
        private readonly IAccountingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalaryQueryHandler> _localizer;
        private readonly IEmployeeService _employeeService;

        public PayslipQueryHandler(
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
        public async Task<PaginatedResult<PayrollDto>> Handle(GetPayslipRequestQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var queryable = _context.Payrolls.AsNoTracking().AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderByDescending(a => a.StartDate);

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
                throw new AccountingException(_localizer["Payslip Not Found!"], HttpStatusCode.NotFound);
            }


            var employeesIds = result.Data.Select(x => x.EmployeeId).ToList();
            var employees = await _employeeService.GetEmployeeDetailsAsync(employeesIds);
            var dto = _mapper.Map<PaginatedResult<PayrollDto>>(result);

            foreach (var item in dto.Data)
            {
                item.EmployeeName = employees.FirstOrDefault(x => x.Id == item.EmployeeId)?.FullName;
            }

            return dto;
        }



#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<PayrollDto>> Handle(GetPayslipRequestByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var payrollRequest = await _context.Payrolls.Where(c => c.Id == query.Id).Include(x=>x.Transactions).FirstOrDefaultAsync(cancellationToken);
            if (payrollRequest == null)
            {
                throw new AccountingException(_localizer["Payroll Request Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedPayrollRequest = _mapper.Map<PayrollDto>(payrollRequest);
            var employee = await _employeeService.GetEmployeeDetailsAsync(mappedPayrollRequest.EmployeeId);
            if (employee != null)
            {
                mappedPayrollRequest.EmployeeName = employee.FullName;
            }

            return await Result<PayrollDto>.SuccessAsync(mappedPayrollRequest);
        }

    }
}