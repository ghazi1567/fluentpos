// --------------------------------------------------------------------------------------------------
// <copyright file="GetBrandsQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Organization.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using FluentPOS.Shared.DTOs.Organizations.Branchs;
using FluentPOS.Shared.DTOs.Organizations.Departments;
using MediatR;
using System;

namespace FluentPOS.Modules.Organizations.Core.Features
{
    public class GetDepartmentsQuery : IRequest<PaginatedResult<DepartmentDto>>
    {
        public int PageNumber { get;  set; }

        public int PageSize { get;  set; }

        public string[] OrderBy { get;  set; }

        public string SearchString { get; private set; }

        public Guid? OrganizationId { get; set; }

        public Guid? BranchId { get; set; }
    }
}