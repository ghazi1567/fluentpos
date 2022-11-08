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
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Salaries.Queries
{
    internal class SalaryPerksQueryHandler :
        IRequestHandler<GetSalaryPerksQuery, PaginatedResult<SalaryPerksDto>>,
        IRequestHandler<GetSalaryPerksByIdQuery, Result<SalaryPerksDto>>
    {
        private readonly IAccountingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalaryQueryHandler> _localizer;

        public SalaryPerksQueryHandler(
            IAccountingDbContext context,
            IMapper mapper,
            IStringLocalizer<SalaryQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<SalaryPerksDto>> Handle(GetSalaryPerksQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var queryable = _context.Salaries.AsNoTracking().OrderBy(x => x.Id).AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            var result = await queryable
               .AsNoTracking()
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);


            if (result == null)
            {
                throw new AccountingException(_localizer["Salary Perks Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<SalaryPerksDto>>(result);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<SalaryPerksDto>> Handle(GetSalaryPerksByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var employee = await _context.Salaries.Where(c => c.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (employee == null)
            {
                throw new AccountingException(_localizer["Salary Perks Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedEmployee = _mapper.Map<SalaryPerksDto>(employee);
            return await Result<SalaryPerksDto>.SuccessAsync(mappedEmployee);
        }

    }
}