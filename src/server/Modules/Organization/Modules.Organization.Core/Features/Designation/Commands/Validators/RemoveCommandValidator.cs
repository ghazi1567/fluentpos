﻿// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveBrandCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Organization.Core.Features.Stores.Commands.Validators
{
    public class RemoveDesignationCommandValidator : AbstractValidator<RemoveDesignationCommand>
    {
        public RemoveDesignationCommandValidator(IStringLocalizer<RemoveCommandValidator> localizer)
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage(_ => localizer["The {PropertyName} property cannot be empty."]);
        }
    }
}