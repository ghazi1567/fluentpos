// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedProductFilter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.DTOs.Catalogs.Products
{
    public class PaginatedProductFilter : PaginatedFilter
    {
        public bool BypassCache { get; set; }

        public string SearchString { get; set; }

        public long[] BrandIds { get; set; }

        public long[] CategoryIds { get; set; }

    }
}