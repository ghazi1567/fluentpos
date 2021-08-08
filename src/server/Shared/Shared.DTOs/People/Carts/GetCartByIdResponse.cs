﻿// <copyright file="GetCartByIdResponse.cs" company="Fluentpos">
// --------------------------------------------------------------------------------------------------
// Copyright (c) Fluentpos. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// --------------------------------------------------------------------------------------------------
// </copyright>

using System;

namespace FluentPOS.Shared.DTOs.People.Carts
{
    public record GetCartByIdResponse(Guid Id, Guid CustomerId, DateTime Timestamp);
}
