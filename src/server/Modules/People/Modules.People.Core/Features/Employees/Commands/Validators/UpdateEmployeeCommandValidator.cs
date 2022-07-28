﻿// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomerCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Customers.Commands.Validators
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateEmployeeCommandValidator(IStringLocalizer<UpdateCustomerCommandValidator> localizer)
        {
            RuleFor(c => c.Id)
                  .NotEqual(Guid.Empty).WithMessage(x => localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                .Length(2, 30).WithMessage(localizer["The {PropertyName} property must have between 2 and 30 characters."]);
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(c => c.Type)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
        }
    }
}