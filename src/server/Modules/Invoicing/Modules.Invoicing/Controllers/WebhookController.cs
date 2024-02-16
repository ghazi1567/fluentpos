using FluentPOS.Modules.Invoicing.Infrastructure.Services;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal class WebhookController : BaseController
    {
        private readonly IWebhookService _webhookService;
        private readonly IWhatsappService _whatsappService;

        public WebhookController(
            IWebhookService webhookService,
            IWhatsappService whatsappService)
        {
            _webhookService = webhookService;
            _whatsappService = whatsappService;
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

        [HttpGet("meta")]
        public ActionResult<string> ConfigureWhatsAppMessageWebhook(
            [FromQuery(Name = "hub.mode")] string hubMode,
            [FromQuery(Name = "hub.challenge")] int hubChallenge,
            [FromQuery(Name = "hub.verify_token")] string hubVerifyToken)
        {
            return Ok(hubChallenge);
        }

        [HttpPost("meta")]
        public async Task<IActionResult> ReceiveWhatsAppTextMessage()
        {
            // var reader = new StreamReader(Request.Body);
            // string requestBody = reader.ReadToEnd();
            // var textMessageReceived = JsonConvert.DeserializeObject<TextMessageReceived>(requestBody);

            await _whatsappService.ProcessEvent(Request);

            return Ok();
        }
    }
}