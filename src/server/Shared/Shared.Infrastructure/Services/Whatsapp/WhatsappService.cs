using FluentPOS.Shared.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.DTOs.Dtos.WhatsApp;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Infrastructure.Services.Whatsapp
{
    public class WhatsappService : IWhatsappService
    {
        private readonly IApplicationDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _accessToken = "EAAUrk9lBHNYBO8L9MqvhTrYaTlfTPg46f81HVYbOOLCeM6p7sySyup6POZArx4D343JnygdxVL9rcdCQkc0BZCLApVoohonwoJq4ZBeWc0aTg7SZCjbH3VMmKeAOFqDLNsjtt7Xt6Q3aQ7HH3831hPgGyiJav2tbLmrCZAucyFTLwkAZCozcHCtArxw6Q4pqAZAsGX2xtp97ZA04owvX";
        private readonly string _whatsAppBusinessEndpoint = "https://graph.facebook.com/v17.0/144615168746385/messages";
        private readonly IShareConfirmationService _shareConfirmationService;
        private readonly IStoreService _storeService;

        public WhatsappService(
            IApplicationDbContext context,
            IHttpClientFactory httpClientFactory,
            IShareConfirmationService shareConfirmationService,
            IStoreService storeService)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient("WhatsAppBusinessApiClient");
            _shareConfirmationService = shareConfirmationService;
            _storeService = storeService;
        }

        public async Task<bool> ProcessEvent(HttpRequest httpRequest)
        {
            try
            {
                var reader = new StreamReader(httpRequest.Body);
                string requestBody = reader.ReadToEnd();
                var textMessageReceived = JsonConvert.DeserializeObject<TextMessageReceived>(requestBody);
                var waEvent = WAEvent(textMessageReceived);
                waEvent.JsonBody = requestBody;
                waEvent.CreatedAt = DateTimeOffset.Now;

                await SaveEventAsync(waEvent);

                if (waEvent.Status == "Received")
                {
                    if (waEvent.RequestFor == 0 && !string.IsNullOrEmpty(waEvent.RefMessageId))
                    {
                        var orderEvent = await _context.WhatsappEvents.FirstOrDefaultAsync(x => x.MessageId == waEvent.RefMessageId);
                        if (orderEvent != null)
                        {
                            waEvent.RefMessageId = orderEvent.MessageId;
                            waEvent.RequestFor = orderEvent.RequestFor;
                        }
                    }

                    if (waEvent.RequestFor > 0)
                    {
                        await _shareConfirmationService.UpdateConfirmation(waEvent);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


        private WhatsappEvent WAEvent(TextMessageReceived textMessage)
        {
            WhatsappEvent whatsappEvent = new WhatsappEvent();
            var entry = textMessage.Entry.FirstOrDefault();
            if (entry != null)
            {
                whatsappEvent.EntryId = entry.Id;
                whatsappEvent.Status = "Received";

                var value = entry.Changes.FirstOrDefault().Value;
                if (value != null)
                {
                    whatsappEvent.PhoneNumberId = value.Metadata.PhoneNumberId;
                    if (value.Messages != null && value.Messages.Count > 0)
                    {
                        var msg = value.Messages.FirstOrDefault();
                        if (msg != null)
                        {
                            if (msg.Context != null)
                            {
                                whatsappEvent.RefMessageId = msg.Context.Id;
                            }

                            if (msg.Type == "interactive")
                            {
                                whatsappEvent.Message = msg.Interactive.ButtonReply.Title;
                                whatsappEvent.MessageId = msg.Id;
                                long orderId = 0;
                                long.TryParse(msg.Interactive.ButtonReply.Id.Split("_")[0], out orderId);
                                whatsappEvent.RequestFor = orderId;
                            }

                            if (msg.Type == "text")
                            {
                                whatsappEvent.Message = msg.Text.Body;
                                whatsappEvent.MessageId = msg.Id;
                            }
                        }

                        whatsappEvent.Phone = value.Contacts.FirstOrDefault().WaId;
                        whatsappEvent.Name = value.Contacts.FirstOrDefault().Profile.Name;
                    }

                    if (value.Statuses != null && value.Statuses.Count > 0)
                    {
                        whatsappEvent.MessageId = value.Statuses.FirstOrDefault().Id;
                        whatsappEvent.Phone = value.Statuses.FirstOrDefault().RecipientId;
                        whatsappEvent.Status = value.Statuses.FirstOrDefault().Status;
                    }
                }
            }

            return whatsappEvent;
        }


        private async Task SaveEventAsync(WhatsappEvent waEvent)
        {
            await _context.WhatsappEvents.AddAsync(waEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SendMessageAsync(TextMessageRequest textMessageRequest)
        {
            var result = await PostAsync(textMessageRequest);
            WhatsappEvent whatsappEvent = new WhatsappEvent
            {
                CreatedAt = DateTime.Now,
                JsonBody = result.Response,
                Message = textMessageRequest.Text?.Body,
                Phone = textMessageRequest.To,
                Status = "Sending",
                MessageId = result.Messages.FirstOrDefault().Id,
                RequestFor = textMessageRequest.RequestFor
            };
            await SaveEventAsync(whatsappEvent);
            return result.Success;
        }

        private async Task<TextMessageValue> PostAsync(TextMessageRequest textMessageRequest)
        {
            TextMessageValue result = new TextMessageValue();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            string json = JsonConvert.SerializeObject(textMessageRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_whatsAppBusinessEndpoint, content).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<TextMessageValue>();
                result.Success = true;
            }
            else
            {
                result.Response = await response.Content.ReadAsStringAsync();
                result.Success = false;
            }

            return result;
        }

        public async Task<List<WhatsappEvent>> GetMessagesByPhone(string phoneNumber)
        {
            if (phoneNumber.StartsWith("03"))
            {
                phoneNumber = $"92{phoneNumber.Substring(1)}";
            }

            if (phoneNumber.StartsWith("+92"))
            {
                phoneNumber = $"{phoneNumber.Substring(1)}";
            }

            return await _context.WhatsappEvents.AsNoTracking().Where(x => x.Phone == phoneNumber).ToListAsync();
        }

        public async Task<bool> SendMessage(long orderId, string phoneNumber, string orderNo, string totalAmount, string type = "text")
        {
            if (phoneNumber.StartsWith("03"))
            {
                phoneNumber = $"92{phoneNumber.Substring(1)}";
            }

            if (phoneNumber.StartsWith("+92"))
            {
                phoneNumber = $"{phoneNumber.Substring(1)}";
            }

            string bodyText = @$"Hello! Thank you for placing your order .
Order number {orderNo} Total Amount {totalAmount}.
Please select ""Confirm"" to confirm your order or ""Cancel"" to cancel it. 

Thank you for your attention, and we look forward to hearing back from you soon.

Best regards,
Miniso Pakistan";

            var request = new TextMessageRequest
            {
                RequestFor = orderId,
                To = phoneNumber,
                Type = type
            };

            if (type == "text")
            {
                request.Text = new WhatsAppText
                {
                    PreviewUrl = false,
                    Body = bodyText
                };
            }

            if (type == "Interactive")
            {
                request.Interactive = InteractiveRequest(orderId, bodyText);
            }

            return await SendMessageAsync(request);
        }

        private InteractiveReplyButtonMessage InteractiveRequest(long orderId, string bodyText)
        {
            return new InteractiveReplyButtonMessage
            {
                Header = new ReplyButtonHeader
                {
                    Type = "text",
                    Text = "Miniso Pakistan"
                },
                Body = new ReplyButtonBody
                {
                    Text = bodyText
                },
                Footer = new ReplyButtonFooter
                {
                    Text = "Once order confirmed you can not cancel."
                },
                Action = new ReplyButtonAction
                {
                    Buttons = new List<ReplyButton>()
                    {
                        new ReplyButton
                        {
                            Type = "reply",
                            Reply = new Reply
                            {
                                Id = $"{orderId}_confirmed",
                                Title = "Confirmed"
                            }
                        },
                        new ReplyButton
                        {
                            Type = "reply",
                            Reply = new Reply
                            {
                                Id = $"{orderId}_cancel",
                                Title = "Cancel"
                            }
                        }
                    }
                }
            };
        }
    }
}