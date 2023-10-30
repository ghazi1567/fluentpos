﻿using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Utilities;
using Microsoft.AspNetCore.Http;
using ShopifySharp;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public class ShopifyOrderService : IShopifyOrderService
    {
        private readonly IHttpContextAccessor _accessor;

        public readonly string StoreId;
        public readonly string _shopifyUrl;

        public readonly string _accessToken;
        public readonly OrderService _orderService;

        public ShopifyOrderService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            StoreId = _accessor.HttpContext?.Request?.Headers["store-id"];
            if (!string.IsNullOrEmpty(StoreId))
            {
                string shopifyCreds = EncryptionUtilities.DecryptString(StoreId);
                _shopifyUrl = shopifyCreds.Split("|")[0];
                _accessToken = shopifyCreds.Split("|")[1];
            }

            if (!string.IsNullOrEmpty(_shopifyUrl) && !string.IsNullOrEmpty(_accessToken))
            {
                _orderService = new OrderService(_shopifyUrl, _accessToken);
            }
            else
            {
                throw new System.Exception("Store url or access token missing");
            }
        }

        public async Task CancelOrder(long shopifyId, string reason)
        {
            await _orderService.CancelAsync(shopifyId, new OrderCancelOptions { Reason = reason });
        }

    }
}
