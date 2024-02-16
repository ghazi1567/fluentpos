using FluentPOS.Modules.Inventory.Core.Features.Levels;
using FluentPOS.Modules.Inventory.Core.Features.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using FluentPOS.Shared.DTOs.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Controllers
{
    [ApiVersion("1")]
    internal sealed class LevelController : BaseController
    {

        [HttpPost]
        [Authorize(Policy = Permissions.Orders.Register)]
        public async Task<IActionResult> RegisterAsync(ImportLevelCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Products.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedFilter filter)
        {
            var request = Mapper.Map<GetImportFilesQuery>(filter);
            var products = await Mediator.Send(request);
            return Ok(products);
        }

        // [HttpGet("{id}")]
        // [Authorize(Policy = Permissions.Products.View)]
        // public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<long, Product> filter)
        // {
        //     var request = Mapper.Map<GetProductByIdQuery>(filter);
        //     var product = await Mediator.Send(request);
        //     return Ok(product);
        // }
    }
}