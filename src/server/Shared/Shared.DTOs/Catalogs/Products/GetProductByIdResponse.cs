// --------------------------------------------------------------------------------------------------
// <copyright file="GetProductByIdResponse.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Products
{
    public record GetProductByIdResponse(long Id, string Name, string LocaleName, string BarcodeSymbology, string Detail, long BrandId, long CategoryId, decimal Price, decimal Cost, string ImageUrl, decimal Tax, string TaxMethod, bool IsAlert, decimal AlertQuantity, decimal discountFactor, DateTime? FactorUpdateOn, string location2);
}