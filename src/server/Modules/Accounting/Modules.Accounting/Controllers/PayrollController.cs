using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Customers.Queries;
using FluentPOS.Modules.People.Core.Features.Salaries.Commands;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.DTOs.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Accounting.Controllers
{
    [ApiVersion("1")]
    internal class PayrollController : BaseController
    {

        [HttpGet]
        [Authorize(Policy = Permissions.Payroll.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetPayrollRequestQuery>(filter);
            var customers = await Mediator.Send(request);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Payroll.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, Salary> filter)
        {
            var request = Mapper.Map<GetPayrollRequestByIdQuery>(filter);
            var customer = await Mediator.Send(request);
            return Ok(customer);
        }

        [HttpGet("RunJob/{id}")]
        [Authorize(Policy = Permissions.Payroll.View)]
        public async Task<IActionResult> RunJobAsync(Guid id)
        {
            var command = new RunPayrollJobCommand() { Id = id };
            var customer = await Mediator.Send(command);
            return Ok(customer);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Payroll.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterPayrollCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Payroll.Update)]
        public async Task<IActionResult> UpdateAsync(UpdatePayrollCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Payroll.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemovePayrollCommand(id)));
        }



    }
}