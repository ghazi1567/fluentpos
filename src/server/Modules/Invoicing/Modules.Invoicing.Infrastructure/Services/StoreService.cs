using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public class StoreService : IStoreService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly StoreSettings _storeSettings;

        public StoreService(IHttpContextAccessor accessor, IOptions<StoreSettings> storeSettings)
        {
            _accessor = accessor;
            _storeSettings = storeSettings.Value;
        }

        public string StoreId()
        {
            var storeId = _accessor.HttpContext?.Request?.Headers["store-id"];
            if (!string.IsNullOrEmpty(storeId))
            {
                return EncryptionUtilities.DecryptString(storeId);
            }

            return $"{_storeSettings.ShopifyUrl}|{_storeSettings.AccessToken}";
        }
    }
}