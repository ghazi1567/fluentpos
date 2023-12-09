// --------------------------------------------------------------------------------------------------
// <copyright file="GetByIdCacheableFilter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using FluentPOS.Shared.Core.Attributes;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Shared.Core.Features.Common.Filters
{
    public class CacheableFilter<T> : ICacheable
    {
        [FromQuery]
        public bool BypassCache { get; set; }

        [FromQuery]
        [SwaggerExclude]
        public string CacheKey => CacheKeys.Common.GetEntityByIdCacheKey<T>();

        [FromQuery]
        [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$")]
        public TimeSpan? SlidingExpiration { get; set; }

    }
}