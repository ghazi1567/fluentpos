using FluentPOS.Shared.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}