using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using ShopifySharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public class ShopifyOrderFulFillmentService : IShopifyOrderFulFillmentService
    {
        public readonly string StoreId;
        public readonly string _shopifyUrl;
        public readonly string _accessToken;

        private readonly FulfillmentService _fulfillmentService;
        private readonly FulfillmentOrderService _fulfillmentOrderService;

        public ShopifyOrderFulFillmentService(IStoreService storeService)
        {
            string shopifyCreds = storeService.StoreId();
            _shopifyUrl = shopifyCreds.Split("|")[0];
            _accessToken = shopifyCreds.Split("|")[1];

            _fulfillmentService = new FulfillmentService(_shopifyUrl, _accessToken);
            _fulfillmentOrderService = new FulfillmentOrderService(_shopifyUrl, _accessToken);
        }

        public async Task<List<FulfillmentOrder>> GetFulFillOrder(long orderId)
        {
            // Find open fulfillment orders for this order
            var openFulfillmentOrders = await _fulfillmentOrderService.ListAsync(orderId);
            return openFulfillmentOrders.ToList();
        }

        public async Task<Fulfillment> CompleteFulFillOrderAsync(long orderId, long? fulFillOrderId, TrackingInfo trackingInfo)
        {
            IEnumerable<LineItemsByFulfillmentOrder> lineItems = null;
            if (fulFillOrderId.HasValue)
            {
                lineItems = new LineItemsByFulfillmentOrder[] {
                    new LineItemsByFulfillmentOrder
                    {
                        FulfillmentOrderId = fulFillOrderId.Value,
                    }
                };
            }
            else
            {
                // Find open fulfillment orders for this order
                var openFulfillmentOrders = await _fulfillmentOrderService.ListAsync(orderId);
                openFulfillmentOrders = openFulfillmentOrders.Where(f => f.Status == "open").ToList();

                // Fulfill the line items
                lineItems = openFulfillmentOrders.Select(o => new LineItemsByFulfillmentOrder
                {
                    FulfillmentOrderId = o.Id.Value
                });
            }

            return await _fulfillmentService.CreateAsync(new FulfillmentShipping
            {
                Message = "items are shipping!",
                FulfillmentRequestOrderLineItems = lineItems,
                TrackingInfo = trackingInfo
            });
        }

        public async Task<bool> ChangeLocationAsync(long orderId, long newLocationId)
        {
            var openFulfillmentOrders = await _fulfillmentOrderService.ListAsync(orderId);
            openFulfillmentOrders = openFulfillmentOrders.Where(f => f.Status == "open").ToList();

            foreach (var item in openFulfillmentOrders)
            {
                var fulfillmentOrderMove = await _fulfillmentOrderService.MoveAsync(item.Id.Value, newLocationId);
            }

            return true;
        }

        public async Task PartialFulFillOrder(long orderId, List<long> itemsToFulfill, TrackingInfo trackingInfo)
        {
            var openFulfillmentOrders = await _fulfillmentOrderService.ListAsync(orderId);
            openFulfillmentOrders = openFulfillmentOrders.Where(f => f.Status == "open").ToList();

            var fulfillmentOrderId = openFulfillmentOrders.FirstOrDefault().Id.Value;

            // Get this fulfillment order
            var fulfillmentOrder = await _fulfillmentOrderService.GetAsync(fulfillmentOrderId);

            // In this example, we let the user choose which line items in this particular fulfillment order will be fulfilled
            // Where request = some kind of form post or json model
            var itemIds = itemsToFulfill;

            // Wrap the desired line items together with the fulfillment order id
            var fulfillmentOrderLineItems = new LineItemsByFulfillmentOrder
            {
                FulfillmentOrderId = fulfillmentOrderId,

                // This is where you do a partial fulfillment. Using this property means you're telling Shopify "only fulfill these parts of the fulfillment order".
                FulfillmentRequestOrderLineItems = itemIds.Select(itemId => new FulfillmentRequestOrderLineItem
                {
                    // NOTE: This Id property is not the same as a `FulfillmentOrderLineItem.LineItemId` value!
                    // You want to use `FulfillmentOrderLineItem.Id` instead.
                    Id = itemId,
                    // You need to tell Shopify how much of this single item to fulfill. You must ensure that 
                    // you are not fulfilling more than the fulfillable quantity, or else Shopify will throw an error.
                    // In this example, we just want to fulfill all of the available line item, so we pass in the
                    // entire fulfillable quantity.
                    Quantity = fulfillmentOrder.FulfillmentOrderLineItems
                    .First(li => li.Id == itemId)
                    .FulfillableQuantity
                })
            };

            // Create a new fulfillment using that fulfillment order with partial line items
            var fulfillment = await _fulfillmentService.CreateAsync(new FulfillmentShipping
            {
                Message = "items are shipping!",
                FulfillmentRequestOrderLineItems = new[] { fulfillmentOrderLineItems },
                TrackingInfo = trackingInfo
            });
        }
    }
}