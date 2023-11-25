using FluentPOS.Shared.DTOs.Sales.Orders;
using ShopifySharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Shopify
{
    public interface IShopifyOrderFulFillmentService
    {
        Task<Fulfillment> CompleteFulFillOrderAsync(long orderId, long? fulFillOrderId, TrackingInfo trackingInfo);

        Task<bool> ChangeLocationAsync(long orderId, long newLocationId);

        Task<List<ShopifySharp.FulfillmentOrder>> GetFulFillOrderByOrderId(long orderId);

        Task<ShopifySharp.FulfillmentOrder> GetFulFillOrderById(long fulfillmentId);

        Task<FulfillmentOrderMove> ChangeLocationAsync(long fulfillmentOrderId, SplitOrderPayloadDto splitOrderPayloadDto);

        Task<SplitOrderGraphqlResponse> SplitFulfillment(long fulfillmentOrderId, SplitOrderPayloadDto splitOrderPayloadDto);
    }
}