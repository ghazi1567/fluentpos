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

namespace FluentPOS.Modules.Organizations.Core.Features.Organizations.Queries.Validators
{
    public class PaginatedOrganizationFilterValidator : PaginatedFilterValidator<Guid, Organisation, PaginatedBrachFilter>
    {
        public PaginatedOrganizationFilterValidator(IStringLocalizer<PaginatedOrganizationFilterValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}