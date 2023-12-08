// --------------------------------------------------------------------------------------------------
// <copyright file="GetBrandsQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Organizations.Policies;
using MediatR;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features
{
    public class GetPoliciesQuery : IRequest<PaginatedResult<GetPolicyResponse>>
    {
        public int PageNumber { get;  set; }

        public int PageSize { get;  set; }

        public string[] OrderBy { get; private set; }

        public string SearchString { get; private set; }

        public long? OrganizationId { get; set; }

        public long? BranchId { get; set; }
    }
}