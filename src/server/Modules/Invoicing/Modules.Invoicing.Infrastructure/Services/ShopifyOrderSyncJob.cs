using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Dtos;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Commands;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Utilities;
using FluentPOS.Shared.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using ShopifySharp.Filters;
using ShopifySharp.Lists;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public class ShopifyOrderSyncJob : HangfireService, IShopifyOrderSyncJob
    {
        // private readonly ISalesDbContext _context;
        // private readonly IMapper _mapper;
        // public ShopifyOrderSyncJob(
        //     ISalesDbContext context,
        //     IMapper mapper)
        // {
        //     _context = context;
        //     _mapper = mapper;
        // }

        private readonly IHttpContextAccessor _accessor;

        public readonly string StoreId;
        public readonly string _shopifyUrl;

        public readonly string _accessToken;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IJsonSerializer _jsonSerializer;

        public ShopifyOrderSyncJob(IHttpContextAccessor accessor, IMapper mapper, IMediator mediator, IJsonSerializer jsonSerializer)
        {
            _accessor = accessor;
            _mapper = mapper;
            _mediator = mediator;
            _jsonSerializer = jsonSerializer;

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
    }
}
