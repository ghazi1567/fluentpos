// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomerCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.People.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Enums;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Employees.Commands
{
    public class RegisterAttendanceCommand : IRequest<Result<Guid>>
    {
        public Guid EmployeeId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public AttendanceStatus AttendanceStatus { get; set; }

    }
}