using FluentPOS.Modules.Inventory.Core.Features.Levels;
using FluentPOS.Shared.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Controllers
{
    [ApiVersion("1")]
    internal sealed class LevelController : BaseController
    {

        [HttpPost]
        [Authorize(Policy = Permissions.Sales.Register)]
        public async Task<IActionResult> RegisterAsync(ImportLevelCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}