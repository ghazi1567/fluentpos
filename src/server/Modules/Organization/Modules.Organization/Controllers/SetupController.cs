using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Modules.Organization.Core.Features.Organizations.Commands;
using FluentPOS.Modules.Organizations.Core.Features.Organizations.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Organization.Controllers
{
    [ApiVersion("1")]
    internal class SetupController : BaseController
    {
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Organizations.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<long, Organisation> filter)
        {
            var request = Mapper.Map<GetOrganizationByIdQuery>(filter);
            var brand = await Mediator.Send(request);
            return Ok(brand);
        }

        [HttpGet("Lookup")]
        [Authorize(Policy = Permissions.Common.Lookup)]
        public async Task<IActionResult> GetLookupAsync([FromQuery] PaginatedOrganizationFilter filter)
        {
            var request = Mapper.Map<GetOrganizationsQuery>(filter);
            request.PageSize = 100000;
            request.PageNumber = 0;
            var brands = await Mediator.Send(request);
            return Ok(brands);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Organizations.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedOrganizationFilter filter)
        {
            var request = Mapper.Map<GetOrganizationsQuery>(filter);
            var brands = await Mediator.Send(request);
            return Ok(brands);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Organizations.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterOrganizationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Organizations.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateOrganizationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Organizations.Remove)]
        public async Task<IActionResult> RemoveAsync(long id)
        {
            return Ok(await Mediator.Send(new RemoveOrganizationCommand(id)));
        }
    }
}