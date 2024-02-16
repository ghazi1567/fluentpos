namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public class OrderMarkAsPaidGraphqlResponse
    {
        public OrderMarkAsPaidGraphqlResult orderMarkAsPaid { get; set; }
    }

    public class OrderMarkAsPaidGraphqlResult
    {
        public OrderResult Order { get; set; }

        public object[] UserErrors { get; set; }

        public string __typename { get; set; }
    }

    public class OrderResult
    {
        public string Id { get; set; }

    }
}
