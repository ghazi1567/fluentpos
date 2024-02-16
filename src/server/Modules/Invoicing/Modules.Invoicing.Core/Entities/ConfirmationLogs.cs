using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    public class ConfirmationLogs : BaseEntity
    {
        public long IntOrderId { get; set; }

        public long IntFulfillmentOrderId { get; set; }

        public string Source { get; set; }

        public string Phone { get; set; }

        public string Message { get; set; }

        public string MessageId { get; set; }

        public bool IsSent { get; set; }

        public bool IsDelivered { get; set; }

        public bool IsConfirmed { get; set; }

        public int Retries { get; set; }

        public DateTimeOffset? LastAttempt { get; set; }

        public DateTimeOffset? SentAt { get; set; }

        public DateTimeOffset? DeliveredAt { get; set; }

        public DateTimeOffset? ConfirmedAt { get; set; }
    }
}