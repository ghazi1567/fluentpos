using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands;
using FluentPOS.Shared.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.DTOs.Dtos.WhatsApp;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Services.Confirmation
{
    public class ShareConfirmationService : IShareConfirmationService
    {

        private readonly ISalesDbContext _context;
        private readonly IMediator _mediator;

        public ShareConfirmationService(
            ISalesDbContext salesDbContext,
            IMediator mediator)
        {
            _context = salesDbContext;
            _mediator = mediator;
        }

        public async Task<bool> UpdateConfirmation(WhatsappEvent waEvent)
        {
            var fulfillmentOrder = await _context.FulfillmentOrders.FirstOrDefaultAsync(x => x.Id == waEvent.RequestFor);
            if (fulfillmentOrder != null)
            {
                if (waEvent.Message == "Confirmed")
                {
                    var result = await _mediator.Send(new UpdateOrderStatusCommand(fulfillmentOrder.InternalOrderId, fulfillmentOrder.Id, Shared.DTOs.Sales.Enums.OrderStatus.Confirmed));
                    return result.Succeeded;
                }
                else if (waEvent.Message == "Cancel")
                {
                    var result = await _mediator.Send(new CancelledOrderCommand(fulfillmentOrder.InternalOrderId, fulfillmentOrder.Id, "Cancelled by custmer"));
                    return result.Succeeded;
                }
            }

            return false;
        }
    }

    public class ConfirmationService : IConfirmationService
    {
        private readonly IWhatsappService _whatsappService;

        private readonly ISalesDbContext _context;

        public ConfirmationService(
            IWhatsappService whatsappService,
            ISalesDbContext salesDbContext)
        {
            _whatsappService = whatsappService;
            _context = salesDbContext;
        }

        public async Task<bool> WhatsAppConfirmation(InternalFulfillmentOrder order)
        {
            string phoneNumber = order.FulfillmentOrderDestination.Phone;

            if (phoneNumber.StartsWith("03"))
            {
                phoneNumber = $"92{phoneNumber.Substring(1)}";
            }

            if (phoneNumber.StartsWith("+92"))
            {
                phoneNumber = $"{phoneNumber.Substring(1)}";
            }

            string orderNumber = order.Name;
            decimal? orderAmount = order.TotalPrice;

            string bodyText = @$"Hello! Thank you for placing your order .
Order number {orderNumber} Total Amount {orderAmount}.
Please reply to this message with ""Confirm"" to confirm your order or ""Cancel"" to cancel it. 

Thank you for your attention, and we look forward to hearing back from you soon.

Best regards,
Miniso Pakistan";

            var request = new TextMessageRequest
            {
                RequestFor = order.Id,
                To = phoneNumber,
                Text = new WhatsAppText
                {
                    PreviewUrl = false,
                    Body = bodyText
                }
            };

            return await _whatsappService.SendMessage(order.Id, phoneNumber, order.Name, $"{order.TotalPrice}", "Interactive");
        }
    }
}
