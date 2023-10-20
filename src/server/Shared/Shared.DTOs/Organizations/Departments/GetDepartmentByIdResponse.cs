// --------------------------------------------------------------------------------------------------
// <copyright file="GetBrandByIdResponse.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.DTOs.Organizations.Departments
{
    public record GetDepartmentByIdResponse(Guid Id,
DateTimeOffset? CreatedAt,
DateTimeOffset? UpdatedAt,
Guid OrganizationId,
Guid BranchId,
string Name,
bool IsGlobalDepartment,
string Description,
Guid? HeadOfDepartment,
int Production);
}