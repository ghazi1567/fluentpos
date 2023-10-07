// --------------------------------------------------------------------------------------------------
// <copyright file="CustomersController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Features.Customers.Queries;
using FluentPOS.Shared.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.People.Controllers
{
    [ApiVersion("1")]
    internal sealed class DashboardController : BaseController
    {
        [HttpGet("AttendanceStats")]
        [Authorize(Policy = Permissions.Common.DashboardAttendanceStats)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetDashboardQuery request)
        {
            var customers = await Mediator.Send(request);
            return Ok(customers);
        }

    }
}