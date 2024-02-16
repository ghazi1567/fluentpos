using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Modules.Invoicing.Core.Features.StockIn.Commands;
using FluentPOS.Shared.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal sealed class StockOutController : BaseController
    {
        [HttpGet]
        [Authorize(Policy = Permissions.Orders.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetPurchaseOrderQuery request)
        {
            var sales = await Mediator.Send(request);
            return Ok(sales);
        }



        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Orders.View)]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await Mediator.Send(new GetStockOutByIdQuery { Id = id }));
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Orders.Register)]
        public async Task<IActionResult> RegisterPOAsync(RegisterStockOutCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("Approve")]
        [Authorize(Policy = Permissions.Orders.Register)]
        public async Task<IActionResult> ApprovePOAsync(ApproveStockOutCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Orders.Remove)]
        public async Task<IActionResult> RemoveAsync(long id)
        {
            return Ok(await Mediator.Send(new RemoveStockOutCommand(id)));
        }


        [HttpPut]
        [Authorize(Policy = Permissions.Orders.Register)]
        public async Task<IActionResult> UpdatePOAsync(UpdateStockOutCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
