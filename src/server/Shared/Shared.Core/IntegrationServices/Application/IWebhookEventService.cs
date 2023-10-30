using FluentPOS.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Application
{
    public interface IWebhookEventService
    {
        public Task<bool> SaveEvent(WebhookEvent webhookEvent);

        Task<List<WebhookEvent>> FetchPendingOrders();

        Task<bool> UpdateStatus(Guid id, string status, string notes, long? ShopifyId);
    }
}