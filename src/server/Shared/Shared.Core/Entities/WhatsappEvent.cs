using System;
using System.ComponentModel.DataAnnotations;

namespace FluentPOS.Shared.Core.Entities
{
    public class WhatsappEvent
    {
        [Key]
        public long Id { get; set; }

        public string Phone { get; set; }

        public string PhoneNumberId { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public string JsonBody { get; set; }

        public string Status { get; set; }

        public string EntryId { get; set; }

        public string MessageId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public long RequestFor { get; set; }

        public string RefMessageId { get; set; }
    }
}
