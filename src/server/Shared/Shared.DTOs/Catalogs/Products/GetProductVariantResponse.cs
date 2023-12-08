// --------------------------------------------------------------------------------------------------
// <copyright file="GetProductsResponse.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Products
{
    public record GetProductVariantResponse(
        long Id,
        long? ShopifyId,
        long? ProductId,
        string Title,
        string SKU,
        long? InventoryItemId,
        long ProductId1);
}