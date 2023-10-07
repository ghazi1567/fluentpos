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
using FluentPOS.Shared.DTOs.Organizations.Designations;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features
{
    public class GetJobsQuery : IRequest<PaginatedResult<JobDto>>
    {
        public int PageNumber { get;  set; }

        public int PageSize { get;  set; }

        public string[] OrderBy { get; private set; }

        public string SearchString { get; private set; }
    }
}