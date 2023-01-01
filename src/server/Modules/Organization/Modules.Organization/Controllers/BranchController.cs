﻿using FluentPOS.Modules.Catalog.Core.Features.Branchs.Queries;
using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Modules.Organization.Core.Features.Branchs.Commands;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.DTOs.Organizations.Branchs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Organization.Controllers
{
    [ApiVersion("1")]
    internal class BranchController : BaseController
    {
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Branchs.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, Branch> filter)
        {
            var request = Mapper.Map<GetBranchByIdQuery>(filter);
            var brand = await Mediator.Send(request);
            return Ok(brand);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Branchs.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedBrachFilter filter)
        {
            var request = Mapper.Map<GetBranchsQuery>(filter);
            var brands = await Mediator.Send(request);
            return Ok(brands);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Branchs.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterBranchCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Branchs.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateBranchCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Branchs.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveBranchCommand(id)));
        }
    }
}