﻿// --------------------------------------------------------------------------------------------------
// <copyright file="GetBrandByIdResponse.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.DTOs.Organizations.Branchs
{
    public record GetBranchByIdResponse(Guid Id,

DateTime? CreateaAt,

DateTime? UpdatedAt,

Guid OrganizationId,

string Name,

string Address,

string PhoneNo,

string EmailAddress,

string Currency,

string Country);
}