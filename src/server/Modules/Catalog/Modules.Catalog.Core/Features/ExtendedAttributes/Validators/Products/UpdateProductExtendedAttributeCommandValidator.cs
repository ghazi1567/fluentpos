﻿// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateProductExtendedAttributeCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Products
{
    public class UpdateProductExtendedAttributeCommandValidator : UpdateExtendedAttributeCommandValidator<long, Product>
    {
        public UpdateProductExtendedAttributeCommandValidator(IStringLocalizer<UpdateProductExtendedAttributeCommandValidator> localizer, IJsonSerializer jsonSerializer)
            : base(localizer, jsonSerializer)
        {
            // you can override the validation rules here
        }
    }
}