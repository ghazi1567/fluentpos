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
using FluentPOS.Shared.DTOs.Organizations.Designations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features
{
    internal class DesignationQueryHandler :
        IRequestHandler<GetDesignationsQuery, PaginatedResult<GetDesignationResponse>>,
        IRequestHandler<GetDesignationByIdQuery, Result<GetDesignationByIdResponse>>
    {
        private readonly IOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DesignationQueryHandler> _localizer;

        public DesignationQueryHandler(
            IOrganizationDbContext context,
            IMapper mapper,
            IStringLocalizer<DesignationQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetDesignationResponse>> Handle(GetDesignationsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Designation, GetDesignationResponse>> expression = e => new GetDesignationResponse(e.UUID, e.CreatedAt, e.UpdatedAt, e.OrganizationId, e.BranchId,e.Name,e.DepartmentId);
            var queryable = _context.Designations.AsNoTracking().AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.UUID);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.UUID.ToString().ToLower(), $"%{request.SearchString.ToLower()}%"));
            }

            if (request.OrganizationId.HasValue)
            {
                queryable = queryable.Where(x => x.OrganizationId == request.OrganizationId.Value);
            }

            if (request.BranchId.HasValue)
            {
                queryable = queryable.Where(x => x.BranchId == request.BranchId.Value);
            }

            var brandList = await queryable
            .Select(expression)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            if (brandList == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Designation)}s Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetDesignationResponse>>(brandList);
        }

        public async Task<Result<GetDesignationByIdResponse>> Handle(GetDesignationByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Designations.AsNoTracking()
                .Where(b => b.UUID == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Designation)} Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedBrand = _mapper.Map<GetDesignationByIdResponse>(entity);
            return await Result<GetDesignationByIdResponse>.SuccessAsync(mappedBrand);
        }


    }
}