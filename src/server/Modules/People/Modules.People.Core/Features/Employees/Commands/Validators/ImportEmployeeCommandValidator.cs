// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterCustomerCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.People.Core.Dtos;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;

namespace FluentPOS.Modules.People.Core.Features.Employees.Commands.Validators
{
    public class ImportEmployeeCommandValidator : AbstractValidator<ImportEmployeeCommand>
    {
        public ImportEmployeeCommandValidator(IStringLocalizer<ImportEmployeeCommandValidator> localizer)
        {
            RuleForEach(x => x.Employees).SetValidator(new EmployeeValidator(localizer));
        }

        public class EmployeeValidator : AbstractValidator<EmployeeDto>
        {
            public EmployeeValidator(IStringLocalizer<ImportEmployeeCommandValidator> localizer)
            {
                RuleFor(c => c.FullName)
                 .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                 .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);

                RuleFor(c => c.FirstName)
                    .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                    .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
                RuleFor(c => c.LastName)
                  .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                  .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
                RuleFor(c => c.FatherName)
                  .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                  .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
                RuleFor(c => c.Gender)
                  .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                  .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
                RuleFor(c => c.EmployeeStatus)
                  .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                  .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);

                RuleFor(c => c.DepartmentId)
                 .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);

                RuleFor(c => c.DesignationId)
               .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
                RuleFor(c => c.PolicyId)
               .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
            }
        }

    }
}