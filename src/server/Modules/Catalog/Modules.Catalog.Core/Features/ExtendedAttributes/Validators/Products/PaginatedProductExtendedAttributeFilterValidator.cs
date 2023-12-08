﻿// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedProductExtendedAttributeFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Products
{
    public class PaginatedProductExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<long, Product>
    {
        public PaginatedProductExtendedAttributeFilterValidator(IStringLocalizer<PaginatedProductExtendedAttributeFilterValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}