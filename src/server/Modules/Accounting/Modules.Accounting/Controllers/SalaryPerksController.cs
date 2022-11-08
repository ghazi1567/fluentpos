using System;
using System.Threading.Tasks;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Customers.Queries;
using FluentPOS.Modules.People.Core.Features.Salaries.Commands;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.DTOs.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Accounting.Controllers
{
    [ApiVersion("1")]
    internal sealed class SalaryPerksController : BaseController
    {

        [HttpGet]
        [Authorize(Policy = Permissions.Employees.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetSalaryPerksQuery>(filter);
            var customers = await Mediator.Send(request);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Employees.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, Salary> filter)
        {
            var request = Mapper.Map<GetSalaryPerksByIdQuery>(filter);
            var customer = await Mediator.Send(request);
            return Ok(customer);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Employees.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterSalaryPerksCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Employees.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateSalaryPerksCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Employees.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveSalaryPerksCommand(id)));
        }
    }
}
