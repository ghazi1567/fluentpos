using ShopifySharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Shopify
{
    public interface IShopifyOrderFulFillmentService
    {
        Task<Fulfillment> CompleteFulFillOrderAsync(long orderId, long? fulFillOrderId, TrackingInfo trackingInfo);

        Task<bool> ChangeLocationAsync(long orderId, long newLocationId);

        Task<List<FulfillmentOrder>> GetFulFillOrder(long orderId);
    }
}