using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal class WarehouseController : BaseController
    {
        private readonly IShopifyLocationService _shopifyLocationService;

        public WarehouseController(IShopifyLocationService shopifyLocationService)
        {
            _shopifyLocationService = shopifyLocationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync()
        {
            return Ok(await Mediator.Send(new GetWarehouseQuery()));
        }

        [HttpGet("Sync")]
        public async Task<IActionResult> SyncAsync()
        {
            return Ok(await _shopifyLocationService.SyncLocations());
        }
    }
}