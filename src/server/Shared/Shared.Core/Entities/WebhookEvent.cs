﻿using System;
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
        }

        [Key]
        public long Id { get; set; }

        public string WebhookId { get; set; }

        public DateTime TriggeredAt { get; set; }

        public string EventEntity { get; set; }

        public string EventOperation { get; set; }

        public string JsonBody { get; set; }

        public string ShopDomain { get; set; }

        public string ApiVersion { get; set; }

        public bool IsTest { get; set; }

        public long? ShopifyId { get; set; }

        public string Status { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public string Note { get; set; }
    }
}