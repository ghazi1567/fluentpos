// --------------------------------------------------------------------------------------------------
// <copyright file="GetProductsQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using FluentPOS.Shared.DTOs.Filters;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    public class GetProductsQuery : CacheableFilter<GetProductsResponse>, IRequest<PaginatedResult<GetProductsResponse>>
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string SearchString { get; private set; }

        public long[] BrandIds { get; private set; }

        public long[] CategoryIds { get; private set; }

        public string[] OrderBy { get; private set; }

        public string AdvancedSearchType { get; set; } = "And";

        public List<FilterModel> AdvanceFilters { get; set; }

        public List<SortModel> SortModel { get; set; }

    }
}