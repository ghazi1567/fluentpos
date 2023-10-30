using FluentPOS.Shared.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Infrastructure.Services
{
    public class WebhookEventService : IWebhookEventService
    {
        private readonly IApplicationDbContext _context;
        private readonly IStringLocalizer<EntityReferenceService> _localizer;

        public WebhookEventService(
            IApplicationDbContext context,
            IStringLocalizer<EntityReferenceService> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<bool> SaveEvent(WebhookEvent webhookEvent)
        {
            await _context.WebhookEvents.AddAsync(webhookEvent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatus(Guid id, string status, string notes, long? ShopifyId)
        {
            var webHookEvent = await _context.WebhookEvents.SingleOrDefaultAsync(x => x.Id == id);
            if (webHookEvent != null)
            {
                webHookEvent.Status = status;
                webHookEvent.Note = notes;
                webHookEvent.UpdatedAt = DateTimeOffset.Now;
                webHookEvent.ShopifyId = ShopifyId;

                _context.WebhookEvents.Update(webHookEvent);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<WebhookEvent>> FetchPendingOrders()
        {
            return await _context.WebhookEvents.AsNoTracking().Where(x => x.EventEntity == "orders" && x.EventOperation == "create" && (x.Status == "Pending" || x.Status == "Failed")).ToListAsync();
        }
    }
}