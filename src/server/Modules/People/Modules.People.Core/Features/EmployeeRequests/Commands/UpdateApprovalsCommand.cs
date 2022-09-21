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
    public class UpdateApprovalsCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public Guid ApproverId { get; set; }

        public RequestStatus Status { get; set; }

        public string Comments { get; set; }


    }
}