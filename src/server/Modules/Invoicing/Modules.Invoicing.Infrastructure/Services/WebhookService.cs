using FluentPOS.Shared.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public interface IWebhookService
    {
        Task<bool> ProcessEvent(HttpRequest httpRequest);
    }

    public class WebhookService : IWebhookService
    {
        private readonly IWebhookEventService _webhookEventService;

        public WebhookService(IWebhookEventService webhookEventService)
        {
            _webhookEventService = webhookEventService;
        }

        public async Task<bool> ProcessEvent(HttpRequest httpRequest)
        {
            var webhookEvent = ExtractEventData(httpRequest);
            await _webhookEventService.SaveEvent(webhookEvent);
            return true;
        }

        private WebhookEvent ExtractEventData(HttpRequest httpRequest)
        {
            try
            {
                string hookType = httpRequest.Headers["x-shopify-topic"].ToString();
                string triggeredAt = httpRequest.Headers["x-shopify-triggered-at"].ToString();
                string webhookId = httpRequest.Headers["x-shopify-webhook-id"].ToString();
                string shopDomain = httpRequest.Headers["x-shopify-shop-domain"].ToString();
                string apiVersion = httpRequest.Headers["x-shopify-api-version"].ToString();
                string isTest = httpRequest.Headers["x-shopify-test"].ToString();
                var reader = new StreamReader(httpRequest.Body);
                string requestBody = reader.ReadToEnd();

                string eventEntity = string.Empty;
                string eventOperation = string.Empty;
                if (!string.IsNullOrEmpty(hookType))
                {
                    string[] splittedType = hookType.Split("/");
                    eventEntity = splittedType[0];
                    eventOperation = splittedType.Length > 1 ? splittedType[1] : string.Empty;
                }


                return new WebhookEvent
                {
                    EventEntity = eventEntity,
                    EventOperation = eventOperation,
                    JsonBody = requestBody,
                    TriggeredAt = DateTime.Parse(triggeredAt),
                    WebhookId = webhookId,
                    ShopDomain = shopDomain,
                    ApiVersion = apiVersion,
                    IsTest = bool.Parse(isTest)
                };
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}