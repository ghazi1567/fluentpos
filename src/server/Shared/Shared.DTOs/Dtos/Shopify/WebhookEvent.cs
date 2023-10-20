using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Dtos.Shopify
{
    public class WebhookEventDto
    {
        public string WebhookId { get; set; }

        public DateTime TriggeredAt { get; set; }

        public string EventEntity { get; set; }

        public string EventOperation { get; set; }

        public string JsonBody { get; set; }

        public string ShopDomain { get; set; }

        public string ApiVersion { get; set; }

        public bool IsTest { get; set; }
    }
}