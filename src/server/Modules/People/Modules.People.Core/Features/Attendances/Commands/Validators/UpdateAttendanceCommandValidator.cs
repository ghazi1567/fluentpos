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
    public class UpdateAttendanceCommandValidator : AbstractValidator<UpdateAttendanceCommand>
    {
        public UpdateAttendanceCommandValidator(IStringLocalizer<UpdateEmployeeRequestCommandValidator> localizer)
        {
            RuleFor(c => c.EmployeeId)
               .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);

            RuleFor(c => c.AttendanceDate)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);

            RuleFor(c => c.UUID)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);

            When(customer => customer.AttendanceStatus == Shared.DTOs.Enums.AttendanceStatus.Present, () => {
                RuleFor(x => x.CheckOut).GreaterThan(x => x.CheckIn).WithMessage(localizer["CheckOut time must be greater then CheckIn time."]);
            });
        }
    }
}