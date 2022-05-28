using FluentPOS.Modules.Invoicing.Core.Features.Reports.VarianceReport;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal class ReportsController : BaseController
    {
        [HttpPost("VarianceReport")]
        public async Task<IActionResult> GetVarianceReportAsync(GetVarianceReportQuery command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}