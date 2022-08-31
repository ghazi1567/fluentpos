﻿// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveBrandCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.Organization.Core.Features.Organizations.Commands
{
    public class RemoveOrganizationCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; }

        public RemoveOrganizationCommand(Guid brandId)
        {
            Id = brandId;
        }
    }
}