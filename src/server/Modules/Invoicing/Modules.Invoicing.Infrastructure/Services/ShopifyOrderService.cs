using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.DTOs.Sales.Orders;
using FluentPOS.Shared.Infrastructure.Services.Shopify;
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
        public readonly FulfillmentOrderService _fulfillmentOrderService;

        public ShopifyOrderService(IStoreService storeService)
        {
            string shopifyCreds = storeService.StoreId();
            _shopifyUrl = shopifyCreds.Split("|")[0];
            _accessToken = shopifyCreds.Split("|")[1];

            _orderService = new OrderService(_shopifyUrl, _accessToken);
            _fulfillmentOrderService = new FulfillmentOrderService(_shopifyUrl, _accessToken);
        }

        public async Task CancelOrder(long shopifyId, string reason)
        {
            await _orderService.CancelAsync(shopifyId, new OrderCancelOptions { Reason = reason });
        }

        public async Task CancelFulfillmentOrder(long shopifyId)
        {
            await _fulfillmentOrderService.CancelAsync(shopifyId);
        }

        public async Task<OrderMarkAsPaidGraphqlResponse> MarkOrderAsPaid(long shopifyId)
        {
            var shopifyServiceExtended = new ShopifyServiceExtended(_shopifyUrl, _accessToken);
            var requestResult = await shopifyServiceExtended.MarkOrderAsPaid(shopifyId);

            return requestResult != null && requestResult.orderMarkAsPaid.UserErrors.Length == 0 ? requestResult : null;
        }

        public async Task CloseOrder(long shopifyId)
        {
            await _orderService.CloseAsync(shopifyId);
        }
    }
}
