﻿// --------------------------------------------------------------------------------------------------
// <copyright file="BaseFilter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.DTOs.Filters
{
    public class BaseFilter
    {
        public Guid OrganizationId { get; set; }

        public Guid BranchId { get; set; }
    }
}