using FluentPOS.Modules.Catalog.Core.Features.Stores.Queries;
using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Modules.Organization.Core.Features.Stores.Commands;
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
    internal class StoreController : BaseController
    {
        [HttpGet("Lookup")]
        [Authorize(Policy = Permissions.Common.Lookup)]
        public async Task<IActionResult> GetLookupAsync([FromQuery] PaginatedBrachFilter filter)
        {
            var request = Mapper.Map<GetBranchsQuery>(filter);
            request.PageSize = 100000;
            request.PageNumber = 0;
            var brands = await Mediator.Send(request);
            return Ok(brands);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Common.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<long, Store> filter)
        {
            var request = Mapper.Map<GetBranchByIdQuery>(filter);
            var brand = await Mediator.Send(request);
            return Ok(brand);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Common.ViewAll)]
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
        public async Task<IActionResult> RemoveAsync(long id)
        {
            return Ok(await Mediator.Send(new RemoveBranchCommand(id)));
        }

        [HttpGet("Warehouses/{id}")]
        [Authorize(Policy = Permissions.Branchs.View)]
        public async Task<IActionResult> GetWarehouseByIdAsync(Guid id)
        {
            var brand = await Mediator.Send(new GetWarehouseByIdQuery(id));
            return Ok(brand);
        }
    }
}