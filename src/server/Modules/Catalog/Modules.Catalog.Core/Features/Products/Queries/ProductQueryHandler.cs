// --------------------------------------------------------------------------------------------------
// <copyright file="ProductQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    internal class ProductQueryHandler :
        IRequestHandler<GetProductsQuery, PaginatedResult<GetProductsResponse>>,
        IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>,
        IRequestHandler<GetProductImageQuery, Result<string>>,
        IRequestHandler<SyncProductCommand, Result<string>>,
        IRequestHandler<GetProductImageByIdsQuery, Result<List<GetProductImageByIdResponse>>>,
        IRequestHandler<GetProductBySKUsQuery, Result<List<GetProductVariantResponse>>>,
        IRequestHandler<GetProductsByIds, Result<List<GetProductVariantResponse>>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationSettings _applicationSettings;
        private readonly IStringLocalizer<ProductQueryHandler> _localizer;
        private readonly IShopifyProductSyncJob _shopifyProductSyncJob;

        public ProductQueryHandler(
            ICatalogDbContext context,
            IMapper mapper,
            IOptions<ApplicationSettings> applicationSettings,
            IStringLocalizer<ProductQueryHandler> localizer,
            IShopifyProductSyncJob shopifyProductSyncJob)
        {
            _context = context;
            _mapper = mapper;
            _applicationSettings = applicationSettings.Value;
            _localizer = localizer;
            _shopifyProductSyncJob = shopifyProductSyncJob;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            // _shopifyProductSyncJob.SyncShopifyProducts();
            Expression<Func<Product, GetProductsResponse>> expression = e => new GetProductsResponse(e.Id, e.ShopifyId, e.CreatedAt, e.UpdatedAt, e.OrganizationId, e.BranchId, e.Title, e.BodyHtml, e.PublishedAt, e.Vendor, e.ProductType, e.Handle, e.PublishedScope, e.Tags, e.Status, e.ReferenceNumber);

            var queryable = _context.Products.AsNoTracking()
                .OrderBy(x => x.Id)
                .AsQueryable();

            if (request.AdvanceFilters != null && request.AdvanceFilters.Count > 0)
            {
                queryable = queryable.AdvanceSearch(request.AdvanceFilters, request.AdvancedSearchType);
            }
            if (request.SortModel != null && request.SortModel.Count > 0)
            {
                queryable = queryable.AdvanceSort(request.SortModel);
            }

            var productList = await queryable
                .AsNoTracking()
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (productList == null)
            {
                throw new CatalogException(_localizer["Products Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetProductsResponse>>(productList);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var product = await _context.Products.AsNoTracking()
                .Where(p => p.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                throw new CatalogException(_localizer["Product Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedProduct = _mapper.Map<GetProductByIdResponse>(product);
            return await Result<GetProductByIdResponse>.SuccessAsync(mappedProduct);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<string>> Handle(GetProductImageQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var productImageQueryable = _context.ProductImage.AsQueryable();
            string data = string.Empty;
            data = await productImageQueryable.Where(p => p.Id == query.Id.Value)
                .OrderBy(x => x.Position)
                .Select(x => x.Src)
                .FirstOrDefaultAsync(cancellationToken);


            return await Result<string>.SuccessAsync(data);
        }


#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<List<GetProductImageByIdResponse>>> Handle(GetProductImageByIdsQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            Expression<Func<ProductImage, GetProductImageByIdResponse>> expression = e => new GetProductImageByIdResponse(e.Id, e.ShopifyId, e.ProductId, e.Position, e.Src, e.Filename, e.Attachment, e.Height, e.Width, e.Alt);

            var productImageQueryable = _context.ProductImage.AsQueryable();
            List<GetProductImageByIdResponse> data = await productImageQueryable.Where(p => query.shopifyIds.Contains(p.ProductId))
                .AsNoTracking()
                .OrderBy(x => x.Position)
                .Select(expression)
                .ToListAsync(cancellationToken);

            return await Result<List<GetProductImageByIdResponse>>.SuccessAsync(data);
        }

        public async Task<Result<string>> Handle(SyncProductCommand query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {

            var result = _shopifyProductSyncJob.SyncShopifyProducts();
            return await Result<string>.SuccessAsync(data: result);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<List<GetProductVariantResponse>>> Handle(GetProductBySKUsQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            Expression<Func<ProductVariant, GetProductVariantResponse>> expression = e => new GetProductVariantResponse(e.Id, e.ShopifyId, null, e.Title, e.SKU, e.InventoryItemId, e.ProductId);

            var products = await _context.ProductVariant.AsNoTracking()
                .Where(p => query.SKUs.Contains(p.SKU))
                .Select(expression)
                .ToListAsync(cancellationToken);

            return await Result<List<GetProductVariantResponse>>.SuccessAsync(products);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<List<GetProductVariantResponse>>> Handle(GetProductsByInventoryItemIds query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            Expression<Func<ProductVariant, GetProductVariantResponse>> expression = e => new GetProductVariantResponse(e.Id, e.ShopifyId, null, e.Title, e.SKU, e.InventoryItemId, e.ProductId);

            var products = await _context.ProductVariant.AsNoTracking()
                .Where(p => query.InventoryItemIds.Contains(p.InventoryItemId.Value))
                .Select(expression)
                .ToListAsync(cancellationToken);

            return await Result<List<GetProductVariantResponse>>.SuccessAsync(products);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<List<GetProductVariantResponse>>> Handle(GetProductsByIds query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            // Expression<Func<Product, GetProductVariantResponse>> expression = e => new GetProductVariantResponse(e.Id, e.ShopifyId, e.Variants, e.Title, e.SKU, e.InventoryItemId, e.ProductId1);

            var products = await _context.Products.AsNoTracking()
                .Where(p => query.Ids.Contains(p.Id))
                .Include(x => x.Variants)
                .ToListAsync(cancellationToken);

            List<GetProductVariantResponse> responses = new List<GetProductVariantResponse>();
            foreach (var product in products)
            {
                foreach (var item in product.Variants)
                {
                    responses.Add(new GetProductVariantResponse(item.Id, item.ShopifyId, null, product.Title, item.SKU, item.InventoryItemId, item.ProductId));
                }
            }
            return await Result<List<GetProductVariantResponse>>.SuccessAsync(responses);
        }
    }
}