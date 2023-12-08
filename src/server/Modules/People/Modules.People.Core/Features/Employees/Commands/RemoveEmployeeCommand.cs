// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveCustomerCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Employees.Commands
{
    public class RemoveEmployeeCommand : IRequest<Result<long>>
    {
        public long Id { get; }

        public RemoveEmployeeCommand(long customerId)
        {
            Id = customerId;
        }
    }
}