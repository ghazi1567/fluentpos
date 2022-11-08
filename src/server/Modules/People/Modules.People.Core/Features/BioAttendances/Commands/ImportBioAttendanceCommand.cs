// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateCustomerCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentPOS.Modules.People.Core.Dtos;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Employees.Commands
{
    public class ImportBioAttendanceCommand : IRequest<Result<Guid>>
    {
        public List<BioAttendanceLogDto> AttendanceLogs { get; set; }
    }
}