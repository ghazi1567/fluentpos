// --------------------------------------------------------------------------------------------------
// <copyright file="IProductService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;

namespace FluentPOS.Shared.Core.IntegrationServices.Catalog
{
    public interface IProductService
    {
        Task<Result<GetProductByIdResponse>> GetDetailsAsync(Guid productId);

        Task<Result<List<GetProductImageByIdResponse>>> GetProductImages(List<long?> productId);

        Task<Result<List<GetProductVariantResponse>>> GetProductBySKUs(List<string> SKUs);

        Task<GetProductVariantResponse> GetProductBySKU(string SKU);

        Task<List<GetProductVariantResponse>> GetProductByInventoryItemIds(List<long> inventoryItemIds);

        Task<List<GetProductVariantResponse>> GetProductByIds(List<Guid> inventoryItemIds);
    }
}