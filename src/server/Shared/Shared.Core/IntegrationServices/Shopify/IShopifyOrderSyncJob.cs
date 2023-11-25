namespace FluentPOS.Shared.Core.IntegrationServices.Shopify
{
    public interface IShopifyOrderSyncJob
    {
        string RunOrderWebhook();

        string SyncShopifyOrders();
        string ProcessOrder();
    }
}
