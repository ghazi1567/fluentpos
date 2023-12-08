using FluentPOS.Modules.Invoicing.Core.Features.PO;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.Sales.Orders;
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
    internal sealed class PurchaseOrderController : BaseController
    {
        [HttpGet]
        [Authorize(Policy = Permissions.Sales.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetPurchaseOrderQuery request)
        {
            var sales = await Mediator.Send(request);
            return Ok(sales);
        }



        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Sales.View)]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await Mediator.Send(new GetPurchaseOrderByIdQuery { Id = id }));
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Sales.Register)]
        public async Task<IActionResult> RegisterPOAsync(RegisterPOCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Sales.Remove)]
        public async Task<IActionResult> RemoveAsync(long id)
        {
            return Ok(await Mediator.Send(new RemovePOCommand(id)));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Sales.Register)]
        public async Task<IActionResult> UpdatePOAsync(UpdatePOCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
