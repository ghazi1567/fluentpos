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
    internal sealed class EmployeesController : BaseController
    {
        [HttpGet]
        [Authorize(Policy = Permissions.Employees.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetEmployeesQuery>(filter);
            var customers = await Mediator.Send(request);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Employees.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, Customer> filter)
        {
            var request = Mapper.Map<GetEmployeeByIdQuery>(filter);
            var customer = await Mediator.Send(request);
            return Ok(customer);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Employees.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Employees.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Employees.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveEmployeeCommand(id)));
        }
    }
}