// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedFilter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.DTOs.Enums;
using System;

namespace FluentPOS.Shared.DTOs.Filters
{
    public class PaginatedFilter : BaseFilter
    {
        public string SearchString { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string OrderBy { get; set; }

        public Guid? EmployeeId { get; set; }

        public RequestType? RequestType { get; set; }

        public PaginatedFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginatedFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}