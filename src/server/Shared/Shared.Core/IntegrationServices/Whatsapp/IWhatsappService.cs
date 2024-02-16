using FluentPOS.Shared.Core.Entities;
using FluentPOS.Shared.DTOs.Dtos.WhatsApp;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Shopify
{
    public interface IWhatsappService
    {
        Task<bool> SendMessageAsync(TextMessageRequest textMessageRequest);

        Task<bool> SendMessage(long orderId, string phoneNumber, string orderNo, string totalAmount, string type = "text");

        Task<bool> ProcessEvent(HttpRequest httpRequest);

        Task<List<WhatsappEvent>> GetMessagesByPhone(string phoneNumber);
    }
}