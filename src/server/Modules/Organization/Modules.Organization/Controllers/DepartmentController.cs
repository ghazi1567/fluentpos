using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Modules.Organization.Core.Features;
using FluentPOS.Modules.Organizations.Core.Features;
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
    internal class DepartmentController : BaseController
    {
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Departments.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, Department> filter)
        {
            var request = Mapper.Map<GetDepartmentByIdQuery>(filter);
            var brand = await Mediator.Send(request);
            return Ok(brand);
        }

        [HttpGet("Lookup")]
        [Authorize(Policy = Permissions.Common.Lookup)]
        public async Task<IActionResult> GetLookupAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetDepartmentsQuery>(filter);
            request.PageSize = 100000;
            request.PageNumber = 0;
            var brands = await Mediator.Send(request);
            return Ok(brands);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Departments.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetDepartmentsQuery>(filter);
            var brands = await Mediator.Send(request);
            return Ok(brands);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Departments.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterDepartmentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Departments.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateDepartmentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Departments.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveDepartmentCommand(id)));
        }
    }
}