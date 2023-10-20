using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Entities
{
    public class WebhookEvent
    {
        public WebhookEvent()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string WebhookId { get; set; }

        public DateTime TriggeredAt { get; set; }

        public string EventEntity { get; set; }

        public string EventOperation { get; set; }

        public string JsonBody { get; set; }

        public string ShopDomain { get; set; }

        public string ApiVersion { get; set; }

        public bool IsTest { get; set; }

        public long? ShopifyId { get; set; }

    }
}
