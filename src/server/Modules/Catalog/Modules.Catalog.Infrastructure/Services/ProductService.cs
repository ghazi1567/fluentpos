// --------------------------------------------------------------------------------------------------
// <copyright file="ProductService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentPOS.Modules.Catalog.Core.Features.Products.Queries;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;

namespace FluentPOS.Modules.Catalog.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;

        public ProductService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<GetProductByIdResponse>> GetDetailsAsync(long productId)
        {
            return await _mediator.Send(new GetProductByIdQuery(productId, false));
        }

        public async Task<Result<List<GetProductVariantResponse>>> GetProductBySKUs(List<string> SKUs)
        {
            return await _mediator.Send(new GetProductBySKUsQuery(SKUs));
        }

        public async Task<GetProductVariantResponse> GetProductBySKU(string SKU)
        {
            var result = await _mediator.Send(new GetProductBySKUsQuery(new List<string> { SKU }));
            return result.Data.FirstOrDefault();
        }

        public async Task<Result<List<GetProductImageByIdResponse>>> GetProductImages(List<long?> productId)
        {
            return await _mediator.Send(new GetProductImageByIdsQuery(productId));
        }

        public async Task<List<GetProductVariantResponse>> GetProductByInventoryItemIds(List<long> inventoryItemIds)
        {
            var result = await _mediator.Send(new GetProductsByInventoryItemIds(inventoryItemIds));
            return result.Data;
        }

        public async Task<List<GetProductVariantResponse>> GetProductByIds(List<long> inventoryItemIds)
        {
            var result = await _mediator.Send(new GetProductsByIds(inventoryItemIds));
            return result.Data;
        }
    }
}