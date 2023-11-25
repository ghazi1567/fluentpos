namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public class SplitOrderGraphqlResponse
    {
        public FulfillmentOrderSplitResult fulfillmentOrderSplit { get; set; }
    }

    public class FulfillmentOrderSplitResult
    {
        public FulfillmentOrderSplit[] FulfillmentOrderSplits { get; set; }

        public object[] UserErrors { get; set; }

        public string __typename { get; set; }
    }

    public class FulfillmentOrderSplit
    {
        public FulfillmentOrder FulfillmentOrder { get; set; }

        public FulfillmentOrder RemainingFulfillmentOrder { get; set; }

        public string __typename { get; set; }

    }

    public class FulfillmentOrder
    {
        public string Id { get; set; }

        public string __typename { get; set; }
    }

    public class FulfillmentOrderSplitPayload
    {
        public FulfillmentOrderSplitResult FulfillmentOrderSplit { get; set; }

        public string __typename { get; set; }
    }
}
