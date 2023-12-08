// --------------------------------------------------------------------------------------------------
// <copyright file="ClearCartCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Carts.Commands
{
    public class ClearCartCommand : IRequest<Result<long>>
    {
        public long Id { get; }

        public ClearCartCommand(long cartId)
        {
            Id = cartId;
        }
    }
}