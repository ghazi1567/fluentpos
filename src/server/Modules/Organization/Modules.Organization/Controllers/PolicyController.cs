using FluentPOS.Modules.Catalog.Core.Features;
using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Modules.Organization.Core.Features;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.DTOs.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Organization.Controllers
{
    [ApiVersion("1")]
    internal class PolicyController : BaseController
    {
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Policy.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<long, Policy> filter)
        {
            var request = Mapper.Map<GetPolicyByIdQuery>(filter);
            var brand = await Mediator.Send(request);
            return Ok(brand);
        }

        [HttpGet("Lookup")]
        [Authorize(Policy = Permissions.Common.Lookup)]
        public async Task<IActionResult> GetLookupAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetPoliciesQuery>(filter);
            request.PageSize = 100000;
            request.PageNumber = 0;
            var brands = await Mediator.Send(request);
            return Ok(brands);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Policy.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetPoliciesQuery>(filter);
            var brands = await Mediator.Send(request);
            return Ok(brands);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Policy.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterPolicyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Policy.Update)]
        public async Task<IActionResult> UpdateAsync(UpdatePolicyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Policy.Remove)]
        public async Task<IActionResult> RemoveAsync(long id)
        {
            return Ok(await Mediator.Send(new RemovePolicyCommand(id)));
        }
    }
}