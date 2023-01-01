// --------------------------------------------------------------------------------------------------
// <copyright file="GetCustomersQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.People.Core.Dtos;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Enums;
using FluentPOS.Shared.DTOs.Filters;
using FluentPOS.Shared.DTOs.People.Customers;
using FluentPOS.Shared.DTOs.People.EmployeeRequests;
using FluentPOS.Shared.DTOs.People.Employees;
using MediatR;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetBioAttendanceQuery : IRequest<PaginatedResult<BioAttendanceLogDto>>
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string[] OrderBy { get; private set; }

        public string SearchString { get; private set; }

        public Guid EmployeeId { get; set; }

        public AttendanceStatus? AttendanceStatus { get; set; }

        public string AdvancedSearchType { get; set; }

        public List<FilterModel> AdvanceFilters { get; set; }
    }
}