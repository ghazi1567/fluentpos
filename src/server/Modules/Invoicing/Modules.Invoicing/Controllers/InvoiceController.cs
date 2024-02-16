using FluentPOS.Modules.Invoicing.Core.Features.Sales.Commands;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal class InvoiceController : BaseController
    {
        [HttpGet]
        [Authorize(Policy = Permissions.Orders.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetInvoicesQuery request)
        {
            var sales = await Mediator.Send(request);
            return Ok(sales);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Orders.View)]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery { Id = id }));
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Orders.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterInvoiceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}