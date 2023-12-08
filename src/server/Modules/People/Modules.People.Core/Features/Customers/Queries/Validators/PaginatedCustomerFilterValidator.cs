﻿// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedCustomerFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.People.Customers;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries.Validators
{
    public class PaginatedCustomerFilterValidator : PaginatedFilterValidator<long, Customer, PaginatedCustomerFilter>
    {
        public PaginatedCustomerFilterValidator(IStringLocalizer<PaginatedCustomerFilterValidator> localizer)

            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}