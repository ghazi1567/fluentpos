using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using Microsoft.AspNetCore.Http;
using ShopifySharp;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public class ShopifyOrderService : IShopifyOrderService
    {
        public readonly string StoreId;
        public readonly string _shopifyUrl;

        public readonly string _accessToken;
        public readonly OrderService _orderService;

        public ShopifyOrderService(IStoreService storeService)
        {
            string shopifyCreds = storeService.StoreId();
            _shopifyUrl = shopifyCreds.Split("|")[0];
            _accessToken = shopifyCreds.Split("|")[1];

            _orderService = new OrderService(_shopifyUrl, _accessToken);
        }

        public async Task CancelOrder(long shopifyId, string reason)
        {
            await _orderService.CancelAsync(shopifyId, new OrderCancelOptions { Reason = reason });
        }

    }
}
