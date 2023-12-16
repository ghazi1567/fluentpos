using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Utilities;
using Microsoft.AspNetCore.Http;
using ShopifySharp;
using ShopifySharp.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Infrastructure.Services
{
    public class ShopifyInventoryService : IShopifyInventoryService
    {
        private readonly IHttpContextAccessor _accessor;

        public readonly string StoreId;
        public readonly string _shopifyUrl;

        public readonly string _accessToken;
        public readonly InventoryLevelService _inventoryLevelService;

        public ShopifyInventoryService(IHttpContextAccessor accessor,IStoreService storeService)
        {
            _accessor = accessor;
            StoreId = _accessor.HttpContext?.Request?.Headers["store-id"];
            if (!string.IsNullOrEmpty(StoreId))
            {
                string shopifyCreds = EncryptionUtilities.DecryptString(StoreId);
                _shopifyUrl = shopifyCreds.Split("|")[0];
                _accessToken = shopifyCreds.Split("|")[1];
            }
            else
            {
                string shopifyCreds = storeService.StoreId();
                _shopifyUrl = shopifyCreds.Split("|")[0];
                _accessToken = shopifyCreds.Split("|")[1];
            }

            if (!string.IsNullOrEmpty(_shopifyUrl) && !string.IsNullOrEmpty(_accessToken))
            {
                _inventoryLevelService = new InventoryLevelService(_shopifyUrl, _accessToken);
            }
            else
            {
                throw new System.Exception("Store url or access token missing");
            }
        }

        public async Task<InventoryLevel> AdjustLevel(long? InventoryItemId, long? LocationId, int? AvailableAdjustment)
        {
            return await _inventoryLevelService.AdjustAsync(new InventoryLevelAdjust
            {
                InventoryItemId = InventoryItemId,
                LocationId = LocationId,
                AvailableAdjustment = AvailableAdjustment
            });
        }

        public async Task<InventoryLevel> SetLevel(long? InventoryItemId, long? LocationId, long? Available)
        {
            return await _inventoryLevelService.SetAsync(new InventoryLevel
            {
                InventoryItemId = InventoryItemId,
                LocationId = LocationId,
                Available = Available
            });
        }

        public async Task<InventoryLevel> ListAsync(long InventoryItemId, long LocationId)
        {
            var inventoryItemIds = new List<long>
            {
                InventoryItemId
            };

            var locationIds = new List<long>
            {
                LocationId
            };
            InventoryLevelListFilter inventoryLevelFilter = new InventoryLevelListFilter
            {
                InventoryItemIds = inventoryItemIds,
                LocationIds = locationIds
            };
            var level = await _inventoryLevelService.ListAsync(inventoryLevelFilter);
            return level.Items.FirstOrDefault();
        }
    }
}