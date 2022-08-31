// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterCustomerCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Employees.Commands.Validators
{
    public class RegisterEmployeeCommandValidator : AbstractValidator<RegisterEmployeeCommand>
    {
        public RegisterEmployeeCommandValidator(IStringLocalizer<RegisterEmployeeCommandValidator> localizer)
        {
            RuleFor(c => c.FullName)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
        }
    }
}