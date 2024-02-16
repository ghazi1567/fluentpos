using FluentPOS.Modules.Invoicing.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal class LookupController : BaseController
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("Warehouses")]
        public async Task<IActionResult> Warehouses([FromQuery]Guid userId)
        {
            return Ok(await _lookupService.GetWarehouse(userId));
        }

        [HttpGet("OperationalCity")]
        public async Task<IActionResult> GetOperationalCity()
        {
            return Ok(await _lookupService.GetOperationalCity());
        }
    }
}