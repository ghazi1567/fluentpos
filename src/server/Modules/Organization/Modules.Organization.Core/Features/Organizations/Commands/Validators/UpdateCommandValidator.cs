// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateBrandCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Organization.Core.Features.Organizations.Commands.Validators
{
    public class UpdateCommandValidator : AbstractValidator<UpdateOrganizationCommand>
    {
        public UpdateCommandValidator(IStringLocalizer<UpdateCommandValidator> localizer)
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage(_ => localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(c => c.Name)
              .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
              .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
        }
    }
}