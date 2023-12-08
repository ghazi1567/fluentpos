// --------------------------------------------------------------------------------------------------
// <copyright file="GetCustomerByIdQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Accounting.Core.Dtos;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetSalaryPerksByIdQuery : IRequest<Result<SalaryPerksDto>>, ICacheable
    {
        public long Id { get; protected set; }

        public bool BypassCache { get; protected set; }

        public string CacheKey { get; protected set; }

        public TimeSpan? SlidingExpiration { get; protected set; }
    }
}