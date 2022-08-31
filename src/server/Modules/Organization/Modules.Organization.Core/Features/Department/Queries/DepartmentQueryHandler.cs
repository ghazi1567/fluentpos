// --------------------------------------------------------------------------------------------------
// <copyright file="BrandQueryHandler.cs" company="FluentPOS">
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
using FluentPOS.Modules.Organization.Core.Abstractions;
using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Modules.Organization.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Organizations.Departments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Organizations.Core.Features
{
    internal class DepartmentQueryHandler :
        IRequestHandler<GetDepartmentsQuery, PaginatedResult<GetDepartmentResponse>>,
        IRequestHandler<GetDepartmentByIdQuery, Result<GetDepartmentByIdResponse>>
    {
        private readonly IOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DepartmentQueryHandler> _localizer;

        public DepartmentQueryHandler(
            IOrganizationDbContext context,
            IMapper mapper,
            IStringLocalizer<DepartmentQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetDepartmentResponse>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Department, GetDepartmentResponse>> expression = e => new GetDepartmentResponse(e.Id, e.CreateaAt, e.UpdatedAt, e.OrganizationId, e.BranchId,e.Name,e.IsGlobalDepartment,e.Description,e.HeadOfDepartment);
            var queryable = _context.Departments.AsNoTracking().AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%"));
            }

            var brandList = await queryable
            .Select(expression)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            if (brandList == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Department)}s Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetDepartmentResponse>>(brandList);
        }

        public async Task<Result<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Departments.AsNoTracking()
                .Where(b => b.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Department)} Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedBrand = _mapper.Map<GetDepartmentByIdResponse>(entity);
            return await Result<GetDepartmentByIdResponse>.SuccessAsync(mappedBrand);
        }


    }
}