using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Organization.Controllers
{
    [ApiVersion("1")]
    internal class SetupController : BaseController
    {
        [HttpPost("Setup")]
        public async Task<IActionResult> GetSetup()
        {
            return Ok(true);
        }
    }
}