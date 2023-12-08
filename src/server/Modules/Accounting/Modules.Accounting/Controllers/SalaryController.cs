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
    internal sealed class SalaryController : BaseController
    {

        [HttpGet]
        [Authorize(Policy = Permissions.EmployeesSalary.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetSalaryQuery>(filter);
            var customers = await Mediator.Send(request);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.EmployeesSalary.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<long, Salary> filter)
        {
            var request = Mapper.Map<GetSalaryByIdQuery>(filter);
            var customer = await Mediator.Send(request);
            return Ok(customer);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.EmployeesSalary.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterSalaryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.EmployeesSalary.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateSalaryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.EmployeesSalary.Remove)]
        public async Task<IActionResult> RemoveAsync(long id)
        {
            return Ok(await Mediator.Send(new RemoveSalaryCommand(id)));
        }
    }
}
