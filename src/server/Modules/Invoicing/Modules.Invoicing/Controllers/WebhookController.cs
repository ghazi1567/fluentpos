using FluentPOS.Modules.Invoicing.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal class WebhookController : BaseController
    {
        private readonly IWebhookService _webhookService;

        public WebhookController(IWebhookService webhookService)
        {
            _webhookService = webhookService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessWebhook()
        {
            return Ok(await _webhookService.ProcessEvent(Request));
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessWebhook()
        {
            return Ok(await _webhookService.ProcessEvent(Request));
        }
    }
}