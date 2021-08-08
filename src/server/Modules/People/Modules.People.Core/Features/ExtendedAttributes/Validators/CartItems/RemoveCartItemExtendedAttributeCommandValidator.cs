﻿// <copyright file="RemoveCartItemExtendedAttributeCommandValidator.cs" company="Fluentpos">
// --------------------------------------------------------------------------------------------------
// Copyright (c) Fluentpos. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// --------------------------------------------------------------------------------------------------
// </copyright>

using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.ExtendedAttributes.Validators.CartItems
{
    public class RemoveCartItemExtendedAttributeCommandValidator : RemoveExtendedAttributeCommandValidator<Guid, CartItem>
    {
        public RemoveCartItemExtendedAttributeCommandValidator(IStringLocalizer<RemoveCartItemExtendedAttributeCommandValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}