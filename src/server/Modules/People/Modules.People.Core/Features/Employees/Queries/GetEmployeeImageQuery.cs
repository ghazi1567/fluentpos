﻿// --------------------------------------------------------------------------------------------------
// <copyright file="GetCustomerImageQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetEmployeeImageQuery : IRequest<Result<string>>
    {
        public long Id { get; }

        public GetEmployeeImageQuery(long customerId)
        {
            Id = customerId;
        }
    }
}