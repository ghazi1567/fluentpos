using ShopifySharp;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Shopify
{
    public interface IShopifyInventoryService
    {
        Task<InventoryLevel> AdjustLevel(long? InventoryItemId, long? LocationId, int? AvailableAdjustment);

        Task<InventoryLevel> SetLevel(long? InventoryItemId, long? LocationId, long? Available);

        Task<InventoryLevel> ListAsync(long InventoryItemId, long LocationId);
    }
}