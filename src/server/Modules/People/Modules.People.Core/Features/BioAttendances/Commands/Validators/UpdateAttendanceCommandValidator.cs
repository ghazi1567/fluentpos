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
    public class UpdateBioAttendanceCommandValidator : AbstractValidator<UpdateBioAttendanceCommand>
    {
        public UpdateBioAttendanceCommandValidator(IStringLocalizer<UpdateEmployeeRequestCommandValidator> localizer)
        {
           
        }
    }
}