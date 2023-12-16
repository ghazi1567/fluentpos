// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedFilter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.DTOs.Enums;
using System;
using System.Collections.Generic;

namespace FluentPOS.Shared.DTOs.Filters
{
    public class PaginatedCacheFilter : BaseFilter
    {
        public string SearchString { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string OrderBy { get; set; }

        public long? EmployeeId { get; set; }

        public RequestType? RequestType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string AdvancedSearchType { get; set; }

        public List<FilterModel> AdvanceFilters { get; set; }

        public PaginatedCacheFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginatedCacheFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}