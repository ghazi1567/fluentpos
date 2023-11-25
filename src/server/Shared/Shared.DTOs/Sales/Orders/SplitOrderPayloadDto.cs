using System.Collections.Generic;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public class SplitOrderPayloadDto
    {
        public SplitOrderFOPayloadDto fulfillment_order { get; set; }
    }

    public class SplitOrderFOPayloadDto
    {
        public long new_location_id { get; set; }

        public List<SplitOrderFOLineItemDto> fulfillment_order_line_items { get; set; }
    }

    public class SplitOrderFOLineItemDto
    {
        public long id { get; set; }

        public long quantity { get; set; }
    }

    public class SplitOrderGQPayloadDto
    {
        public SplitOrderFOGQPayloadDto fulfillment_order { get; set; }
    }

    public class SplitOrderFOGQPayloadDto
    {
        public List<SplitOrderFOLineItemGQDto> fulfillmentOrderLineItems { get; set; }

        public string fulfillmentOrderId { get; set; }
    }

    public class SplitOrderFOLineItemGQDto
    {
        public string id { get; set; }

        public long quantity { get; set; }
    }
}