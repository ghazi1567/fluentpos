using Newtonsoft.Json;
using System.Collections.Generic;

namespace FluentPOS.Shared.DTOs.Dtos.WhatsApp
{
    /// <summary>
    /// A text message you received from a customer
    /// </summary>
    public class TextMessageReceived
    {
        public string Object { get; set; }

        public List<TextMessageEntry> Entry { get; set; }
    }
    public class TextMessageEntry
    {
        public string Id { get; set; }

        public List<TextMessageChange> Changes { get; set; }
    }

    public class TextMessageChange
    {
        public TextMessageValue Value { get; set; }

        public string Field { get; set; }
    }

    public class TextMessageValue
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; }

        public TextMessageMetadata Metadata { get; set; }

        public List<TextMessageContact> Contacts { get; set; }

        public List<TextMessage> Messages { get; set; }

        public List<TextStatus> Statuses { get; set; }

        public bool Success { get; set; }

        public string Response { get; set; }

    }

    public class TextMessageContact
    {
        public TextMessageProfile Profile { get; set; }

        [JsonProperty("wa_id")]
        public string WaId { get; set; }
    }

    public class TextMessageProfile
    {
        public string Name { get; set; }
    }

    public class TextMessage
    {
        public string From { get; set; }

        public string Id { get; set; }

        public string Timestamp { get; set; }

        public TextMessageText Text { get; set; }

        public TextMessageInteractive Interactive { get; set; }

        public string Type { get; set; }

        public TextMessageContext? Context { get; set; }
    }

    public class TextMessageText
    {
        public string Body { get; set; }
    }

    public class TextMessageInteractive
    {
        public string Type { get; set; }

        [JsonProperty("button_reply")]
        public ButtonReply ButtonReply { get; set; }
    }

    public class ButtonReply
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }

    public class TextMessageMetadata
    {
        [JsonProperty("display_phone_number")]
        public string DisplayPhoneNumber { get; set; }

        [JsonProperty("phone_number_id")]
        public string PhoneNumberId { get; set; }
    }

    public class TextMessageContext
    {
        public string From { get; set; }

        public string Id { get; set; }
    }

    public class TextStatus
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public string Timestamp { get; set; }

        [JsonProperty("recipient_id")]
        public string RecipientId { get; set; }
    }
}
