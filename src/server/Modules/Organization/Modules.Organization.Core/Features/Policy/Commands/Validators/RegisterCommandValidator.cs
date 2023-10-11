// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterBrandCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.DTOs.Enums;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Organization.Core.Features.Stores.Commands.Validators
{
    public class RegisterPolicyCommandValidator : AbstractValidator<RegisterPolicyCommand>
    {
        public RegisterPolicyCommandValidator(IStringLocalizer<RegisterCommandValidator> localizer)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
            RuleFor(c => c.OrganizationId)
               .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(c => c.BranchId)
               .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);

            RuleFor(c => c.ShiftStartTime)
           .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);

            RuleFor(c => c.ShiftEndTime)
             .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);

            When(c => c.lateComersPenaltyType != LateComersPenaltyType.Nothing, () =>
            {
                RuleFor(customer => customer.lateComersPenalty).GreaterThan(0);
            });

            When(c => c.DailyOverTime == OverTime.FixedRate || c.DailyOverTime == OverTime.SalaryPerHourMultiplyX, () =>
            {
                RuleFor(customer => customer.DailyOverTimeRate).GreaterThan(0);
            });

            When(c => c.HolidayOverTime == OverTime.FixedRate || c.HolidayOverTime == OverTime.SalaryPerHourMultiplyX, () =>
            {
                RuleFor(customer => customer.HolidayOverTimeRate).GreaterThan(0);
            });
        }
    }
}