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
using FluentPOS.Shared.DTOs.Organizations.Branchs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Branchs.Queries
{
    internal class BrandQueryHandler :
        IRequestHandler<GetBranchsQuery, PaginatedResult<GetBranchResponse>>,
        IRequestHandler<GetBranchByIdQuery, Result<GetBranchByIdResponse>>
    {
        private readonly IOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<BrandQueryHandler> _localizer;

        public BrandQueryHandler(
            IOrganizationDbContext context,
            IMapper mapper,
            IStringLocalizer<BrandQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetBranchResponse>> Handle(GetBranchsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Branch, GetBranchResponse>> expression = e => new GetBranchResponse(e.Id, e.CreateaAt, e.UpdatedAt, e.OrganizationId, e.Name, e.Address, e.PhoneNo, e.EmailAddress, e.Currency, e.Country);
            var queryable = _context.Branchs.AsNoTracking().AsQueryable();

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
                throw new OrganizationException(_localizer[$"{nameof(Branch)}s Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetBranchResponse>>(brandList);
        }

        public async Task<Result<GetBranchByIdResponse>> Handle(GetBranchByIdQuery query, CancellationToken cancellationToken)
        {
            var brand = await _context.Branchs.AsNoTracking()
                .Where(b => b.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (brand == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Branch)} Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedBrand = _mapper.Map<GetBranchByIdResponse>(brand);
            return await Result<GetBranchByIdResponse>.SuccessAsync(mappedBrand);
        }


    }
}