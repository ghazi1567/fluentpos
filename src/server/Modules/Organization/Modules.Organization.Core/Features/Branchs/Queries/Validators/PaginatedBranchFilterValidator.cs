// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedBrandFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.Organizations.Branchs;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Branchs.Queries.Validators
{
    public class PaginatedBranchFilterValidator : PaginatedFilterValidator<Guid, Branch, PaginatedBrachFilter>
    {
        public PaginatedBranchFilterValidator(IStringLocalizer<PaginatedBranchFilterValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}