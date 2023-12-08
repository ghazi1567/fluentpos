// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterBrandCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Organization.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;

namespace FluentPOS.Modules.Organization.Core.Features.Organizations.Commands
{
    public class RegisterOrganizationCommand : OrganizationDto, IRequest<Result<long>>
    {
    }
}