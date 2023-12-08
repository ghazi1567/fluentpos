// --------------------------------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentPOS.Modules.Identity.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Identity.Users;

namespace FluentPOS.Modules.Identity.Core.Abstractions
{
    public interface IUserService
    {
        Task<Result<List<UserResponse>>> GetAllAsync();

        Task<IResult<UserResponse>> GetAsync(string userId);

        Task<IResult<UserRolesResponse>> GetRolesAsync(string userId);

        Task<IResult<string>> UpdateAsync(UpdateUserRequest request);

        Task<IResult<string>> UpdateUserRolesAsync(string userId, UserRolesRequest request);

        Task<IResult<string>> UpdateUserBranchsAsync(string userId, UserBranchModel request);

        Task<IResult<UserBranchModel>> GetUserBranchsAsync(long userId);
    }
}