using FluentPOS.Shared.Core.Entities;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Application
{
    public interface IWebhookEventService
    {
        public Task<bool> SaveEvent(WebhookEvent webhookEvent);
    }
}