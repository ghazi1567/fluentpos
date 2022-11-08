// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomerCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Employees.Commands.Validators
{
    public class RegisterAttendanceCommandValidator : AbstractValidator<RegisterAttendanceCommand>
    {
        public RegisterAttendanceCommandValidator(IStringLocalizer<RegisterAttendanceCommandValidator> localizer)
        {
            RuleFor(c => c.EmployeeId)
               .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);

            RuleFor(c => c.AttendanceDate)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);

            RuleFor(c => c.AttendanceStatus)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
        }
    }
}