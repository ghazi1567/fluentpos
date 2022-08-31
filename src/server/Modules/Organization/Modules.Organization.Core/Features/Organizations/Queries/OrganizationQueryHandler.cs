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
using FluentPOS.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Organizations.Core.Features.Organizations.Queries
{
    internal class OrganizationQueryHandler :
        IRequestHandler<GetOrganizationsQuery, PaginatedResult<GetOrganizationResponse>>,
        IRequestHandler<GetOrganizationByIdQuery, Result<GetOrganizationByIdResponse>>
    {
        private readonly IOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<OrganizationQueryHandler> _localizer;

        public OrganizationQueryHandler(
            IOrganizationDbContext context,
            IMapper mapper,
            IStringLocalizer<OrganizationQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetOrganizationResponse>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Organisation, GetOrganizationResponse>> expression = e => new GetOrganizationResponse(e.Id, e.CreateaAt, e.UpdatedAt, e.Name, e.Address, e.PhoneNo, e.EmailAddress, e.Currency, e.Country);
            var queryable = _context.Organisations.AsNoTracking().AsQueryable();

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
                throw new OrganizationException(_localizer[$"{nameof(Organisation)}s Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetOrganizationResponse>>(brandList);
        }

        public async Task<Result<GetOrganizationByIdResponse>> Handle(GetOrganizationByIdQuery query, CancellationToken cancellationToken)
        {
            var brand = await _context.Organisations.AsNoTracking()
                .Where(b => b.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (brand == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Organisation)} Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedBrand = _mapper.Map<GetOrganizationByIdResponse>(brand);
            return await Result<GetOrganizationByIdResponse>.SuccessAsync(mappedBrand);
        }


    }
}