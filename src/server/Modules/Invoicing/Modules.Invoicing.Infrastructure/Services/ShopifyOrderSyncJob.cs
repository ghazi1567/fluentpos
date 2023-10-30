using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Features.Orders.Commands;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Commands;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Utilities;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.Infrastructure.Services;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopifySharp.Filters;
using ShopifySharp.Lists;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public class ShopifyOrderSyncJob : HangfireService, IShopifyOrderSyncJob
    {
        private const string WebhookOrderJobId = "ShopifyOrderJob1";

        private readonly IHttpContextAccessor _accessor;

        public readonly string StoreId;
        public readonly string _shopifyUrl;

        public readonly string _accessToken;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IWebhookEventService _webhookEventService;

        public ShopifyOrderSyncJob(IHttpContextAccessor accessor, IMapper mapper, IMediator mediator, IJsonSerializer jsonSerializer, IWebhookEventService webhookEventService)
        {
            _accessor = accessor;
            _mapper = mapper;
            _mediator = mediator;
            _jsonSerializer = jsonSerializer;
            _webhookEventService = webhookEventService;
            StoreId = _accessor.HttpContext?.Request?.Headers["store-id"];
            if (!string.IsNullOrEmpty(StoreId))
            {
                string shopifyCreds = EncryptionUtilities.DecryptString(StoreId);
                _shopifyUrl = shopifyCreds.Split("|")[0];
                _accessToken = shopifyCreds.Split("|")[1];
            }
        }

        public async Task<bool> FetchAndSaveShopifyOrders(string shopifyUrl, string accessToken, ListFilter<ShopifySharp.Order> filter = null)
        {
            try
            {
                var service = new ShopifySharp.OrderService(shopifyUrl, accessToken);
                ListResult<ShopifySharp.Order> orders = null;

                if (filter != null)
                {
                    orders = await service.ListAsync(filter);
                }
                else
                {

                    orders = await service.ListAsync();
                }


                await SaveOrders(orders);

                if (orders.HasNextPage)
                {
                    await FetchAndSaveShopifyOrders(shopifyUrl, accessToken, orders.GetNextPageFilter());
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public string SyncShopifyOrders()
        {
            return Enqueue(() => FetchAndSaveShopifyOrders(_shopifyUrl, _accessToken, null));
        }

        public async Task SaveOrders(ListResult<ShopifySharp.Order> orders)
        {
            if (orders.Items.Count() > 0)
            {
                foreach (var shopifyProduct in orders.Items)
                {
                    try
                    {
                        var order = _mapper.Map<RegisterOrderCommand>(shopifyProduct);
                        order.Status = Shared.DTOs.Sales.Enums.OrderStatus.Pending;
                        var response = await _mediator.Send(order);
                        // Console.WriteLine(response.Messages);
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }


        public string RunOrderWebhook()
        {
            RemoveIfExists(WebhookOrderJobId);
            ScheduleRecurring(methodCall: () => FetchWebhookEvents(), recurringJobId: WebhookOrderJobId, cronExpression: Cron.MinuteInterval(5));
            return WebhookOrderJobId;
        }

        public async Task FetchWebhookEvents()
        {
            var pendingOrders = await _webhookEventService.FetchPendingOrders();
            foreach (var order in pendingOrders)
            {
                try
                {
                    var shopifyOrder = JsonConvert.DeserializeObject<ShopifySharp.Order>(order.JsonBody);
                    var command = _mapper.Map<RegisterOrderCommand>(shopifyOrder);
                    var response = await SaveOrder(command);
                    if (response.Succeeded)
                    {
                        await _webhookEventService.UpdateStatus(order.Id, "Completed", "Order Saved Successully", command.ShopifyId);
                    }
                    else
                    {
                        string msg = string.Join(", ", response.Messages);
                        await _webhookEventService.UpdateStatus(order.Id, msg.Contains("Duplicate") ? "Duplicate" : "Failed", msg, command.ShopifyId);
                    }
                }
                catch (Exception ex)
                {
                    await _webhookEventService.UpdateStatus(order.Id, "Failed", ex.Message, null);
                }
            }
        }

        public async Task<Result<Guid>> SaveOrder(RegisterOrderCommand registerOrderCommand)
        {
            return await _mediator.Send(registerOrderCommand);
        }
    }
}