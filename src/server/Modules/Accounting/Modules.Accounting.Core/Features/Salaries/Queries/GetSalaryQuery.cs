// --------------------------------------------------------------------------------------------------
// <copyright file="GetCustomersQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Accounting.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Enums;
using MediatR;
using System;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetSalaryQuery : IRequest<PaginatedResult<SalaryDto>>
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string[] OrderBy { get; private set; }

        public string SearchString { get; private set; }

        public long EmployeeId { get; set; }

        public RequestType? RequestType { get; set; }

        public long? OrganizationId { get; set; }

        public long? BranchId { get; set; }
    }
}