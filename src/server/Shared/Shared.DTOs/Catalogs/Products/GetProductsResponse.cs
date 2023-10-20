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
    public record GetProductsResponse(
        Guid Id,
        long? ShopifyId,
        DateTimeOffset? CreatedAt,
        DateTimeOffset? UpdatedAt,
        Guid OrganizationId,
        Guid BranchId,
        string Title,
        string BodyHtml,
        DateTimeOffset? PublishedAt,
        string Vendor,
        string ProductType,
        string Handle,
        string PublishedScope,
        string Tags,
        string Status,
        string ReferenceNumber);
}