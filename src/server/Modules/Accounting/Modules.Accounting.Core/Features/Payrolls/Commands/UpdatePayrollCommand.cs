// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomerCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Accounting.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Salaries.Commands
{
    public class UpdatePayrollCommand : PayrollRequestDto, IRequest<Result<Guid>>
    {
    }
}