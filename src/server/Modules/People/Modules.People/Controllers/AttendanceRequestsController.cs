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
    internal sealed class AttendanceRequestsController : BaseController
    {
        [HttpGet("MyQueue")]
        [Authorize(Policy = Permissions.AttendanceRequests.MyQueue)]
        public async Task<IActionResult> GetMyQueueAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetMyQueueQuery>(filter);
            var customers = await Mediator.Send(request);
            return Ok(customers);
        }

        [HttpGet("RequestApprover")]
        [Authorize(Policy = Permissions.AttendanceRequests.ViewAll)]
        public async Task<IActionResult> GetRequestApproverAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetRequestApproverListQuery>(filter);
            var customers = await Mediator.Send(request);
            return Ok(customers);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.AttendanceRequests.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetEmployeeRequestsQuery>(filter);
            var customers = await Mediator.Send(request);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.AttendanceRequests.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, Customer> filter)
        {
            var request = Mapper.Map<GetEmployeeRequestByIdQuery>(filter);
            var customer = await Mediator.Send(request);
            return Ok(customer);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.AttendanceRequests.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterEmployeeRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.AttendanceRequests.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateEmployeeRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.AttendanceRequests.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveEmployeeRequestCommand(id)));
        }

        [HttpPost("UpdateApproval")]
        [Authorize(Policy = Permissions.AttendanceRequests.Update)]
        public async Task<IActionResult> UpdateApprovalAsync(UpdateApprovalsCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("ShiftPlan")]
        [Authorize(Policy = Permissions.ShiftPlans.ViewAll)]
        public async Task<IActionResult> GetShiftPlanAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetShiftPlanningQuery>(filter);
            var customers = await Mediator.Send(request);
            return Ok(customers);
        }

        [HttpPost("ShiftPlan")]
        [Authorize(Policy = Permissions.ShiftPlans.Register)]
        public async Task<IActionResult> CreateShiftPlanAsync(RegisterShiftPlanningCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("ShiftPlan/{id}")]
        [Authorize(Policy = Permissions.ShiftPlans.Remove)]
        public async Task<IActionResult> RemoveShiftPlanAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveShiftPlanningCommand(id)));
        }
    }
}