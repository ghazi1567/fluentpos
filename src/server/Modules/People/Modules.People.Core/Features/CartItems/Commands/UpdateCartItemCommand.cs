﻿// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateCartItemCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Commands
{
    public class UpdateCartItemCommand : IRequest<Result<long>>
    {
        public long Id { get; set; }

        public long CartId { get; set; }

        public long ProductId { get; set; }

        public int Quantity { get; set; }
    }
}