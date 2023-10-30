using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Shopify
{
    public interface IShopifyOrderService
    {
        Task CancelOrder(long shopifyId, string reason);
    }
}
