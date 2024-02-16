using FluentPOS.Shared.DTOs.Sales.Orders;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Shopify
{
    public interface IShopifyOrderService
    {
        Task CancelOrder(long shopifyId, string reason);

        Task CancelFulfillmentOrder(long shopifyId);

        Task<OrderMarkAsPaidGraphqlResponse> MarkOrderAsPaid(long shopifyId);

        Task CloseOrder(long shopifyId);
    }
}
