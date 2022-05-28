using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal class WarehouseController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync()
        {
            return Ok(await Mediator.Send(new GetWarehouseQuery()));
        }

    }
}