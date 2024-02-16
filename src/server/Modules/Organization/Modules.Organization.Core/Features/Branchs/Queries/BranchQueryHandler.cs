// --------------------------------------------------------------------------------------------------
// <copyright file="BrandQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Organization.Core.Abstractions;
using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Modules.Organization.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Utilities;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Organizations.Branchs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Stores.Queries
{
    internal class BrandQueryHandler :
        IRequestHandler<GetBranchsQuery, PaginatedResult<GetBranchResponse>>,
        IRequestHandler<GetBranchByIdQuery, Result<GetBranchByIdResponse>>,
        IRequestHandler<GetWarehouseByIdQuery, Result<List<GetWarehouseByIdResponse>>>
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
            Expression<Func<Store, GetBranchResponse>> expression = e => new GetBranchResponse(e.Id, e.CreatedAt, e.UpdatedAt, e.OrganizationId, e.Name, e.Address, e.PhoneNo, e.EmailAddress, e.Currency, e.Country, EncryptionUtilities.EncryptString($"{e.ShopifyUrl}:{e.AccessToken}"));
            var queryable = _context.Stores.AsNoTracking().AsQueryable();

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
                throw new OrganizationException(_localizer[$"{nameof(Store)}s Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetBranchResponse>>(brandList);
        }

        public async Task<Result<GetBranchByIdResponse>> Handle(GetBranchByIdQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Store, GetBranchByIdResponse>> expression = e => new GetBranchByIdResponse(e.Id, e.CreatedAt, e.UpdatedAt, e.OrganizationId, e.Name, e.Address, e.PhoneNo, e.EmailAddress, e.Currency, e.Country, EncryptionUtilities.EncryptString($"{e.ShopifyUrl}|{e.AccessToken}"));

            var brand = await _context.Stores.AsNoTracking()
                .Where(b => b.Id == query.Id)
                .Select(expression)
                .FirstOrDefaultAsync(cancellationToken);

            if (brand == null)
            {
                throw new OrganizationException(_localizer[$"{nameof(Store)} Not Found!"], HttpStatusCode.NotFound);
            }

            return await Result<GetBranchByIdResponse>.SuccessAsync(brand);
        }

        public async Task<Result<List<GetWarehouseByIdResponse>>> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<StoreWarehouse, GetWarehouseByIdResponse>> expression = e => new GetWarehouseByIdResponse(e.Id, e.OrganizationId, e.BranchId, e.WarehouseId, e.IdentityUserId);

            var brand = await _context.StoreWarehouses.AsNoTracking()
               .Where(b => b.IdentityUserId == request.Id)
               .Select(expression)
               .ToListAsync(cancellationToken);

            return await Result<List<GetWarehouseByIdResponse>>.SuccessAsync(brand);

        }
    }
}