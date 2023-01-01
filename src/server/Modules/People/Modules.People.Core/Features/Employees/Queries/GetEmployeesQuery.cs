// --------------------------------------------------------------------------------------------------
// <copyright file="GetCustomersQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Filters;
using FluentPOS.Shared.DTOs.People.Employees;
using MediatR;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetEmployeesQuery : IRequest<PaginatedResult<GetEmployeesResponse>>
    {
        public int PageNumber { get;  set; }

        public int PageSize { get;  set; }

        public string[] OrderBy { get; private set; }

        public string SearchString { get; private set; }

        public Guid? OrganizationId { get; set; }

        public Guid? BranchId { get; set; }

        public string AdvancedSearchType { get; set; }

        public List<FilterModel> AdvanceFilters { get; set; }

    }
}