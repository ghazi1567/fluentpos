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
    internal class JobController : BaseController
    {
        [HttpGet("Lookup")]
        [Authorize(Policy = Permissions.Common.Lookup)]
        public async Task<IActionResult> GetLookupAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetJobsQuery>(filter);
            request.PageSize = 100000;
            request.PageNumber = 0;
            var brands = await Mediator.Send(request);
            return Ok(brands);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Jobs.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetJobsQuery>(filter);
            var brands = await Mediator.Send(request);
            return Ok(brands);
        }


        [HttpPost]
        [Authorize(Policy = Permissions.Jobs.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterJobCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Jobs.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateJobCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Jobs.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveJobCommand(id)));
        }

        [HttpPost("Run")]
        [AllowAnonymous]
        public async Task<IActionResult> RunAsync(RunJobCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}