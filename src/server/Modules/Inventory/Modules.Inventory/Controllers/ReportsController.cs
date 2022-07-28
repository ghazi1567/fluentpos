using FluentPOS.Modules.Inventory.Core.Features.Reports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Controllers
{
    [ApiVersion("1")]
    internal sealed class ReportsController : BaseController
    {
        [HttpPost("StockReport")]
        public async Task<IActionResult> GetStockReport(StockReportQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("StockOutReport")]
        public async Task<IActionResult> GetStockOutReport(StockOutReportQuery command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}