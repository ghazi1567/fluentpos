// --------------------------------------------------------------------------------------------------
// <copyright file="CustomersController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Customers.Queries;
using FluentPOS.Modules.People.Core.Features.Employees.Commands;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.DTOs.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.People.Controllers
{
    [ApiVersion("1")]
    internal sealed class AttendanceController : BaseController
    {
        [HttpGet]
        [Authorize(Policy = Permissions.Attendance.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetAttendanceQuery>(filter);
            var customers = await Mediator.Send(request);
            return Ok(customers);
        }

        [HttpPost("AdvanceSearch")]
        [Authorize(Policy = Permissions.Attendance.ViewAll)]
        public async Task<IActionResult> GetAllAdvanceSearchAsync(GetAttendanceQuery request)
        {
            var customers = await Mediator.Send(request);
            return Ok(customers);
        }

        [HttpGet("IndividualReport")]
        [Authorize(Policy = Permissions.Attendance.View)]
        public async Task<IActionResult> GetIndividualReportAsync([FromQuery] GetIndividualReportQuery filter)
        {
            // var request = Mapper.Map<GetIndividualReportQuery>(filter);
            var customer = await Mediator.Send(filter);
            return Ok(customer);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Attendance.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterAttendanceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Attendance.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateAttendanceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Attendance.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveEmployeeRequestCommand(id)));
        }

    }
}