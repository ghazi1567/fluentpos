using FluentPOS.Shared.Core.Domain;
using Newtonsoft.Json;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    public class OrderLineItem : BaseEntity
    {
        public int? FulfillableQuantity { get; set; }

        public string FulfillmentService { get; set; }

        public string FulfillmentStatus { get; set; }

        public long? Grams { get; set; }

        public decimal? Price { get; set; }

        public long? ProductId { get; set; }

        public int? Quantity { get; set; }

        public bool? RequiresShipping { get; set; }

        public string SKU { get; set; }

        public string Title { get; set; }

        public long? VariantId { get; set; }

        public string VariantTitle { get; set; }

        public string Name { get; set; }

        public string Vendor { get; set; }

        public bool? GiftCard { get; set; }

        public bool? Taxable { get; set; }

        public string TipPaymentGateway { get; set; }

        public string TipPaymentMethod { get; set; }

        /// <summary>
        /// Whether the tip_payment_gateway field is present or not.  If true, the line is a tip line.
        /// For a tip line, tip_payment_gateway is always specified (though it can be null).
        /// For a non tip line, tip_payment_gateway is never specified.
        /// </summary>
        /// <remarks>
        /// This is a Json.Net feature and not a Shopify API property. Refer to #706 for more details.
        /// </remarks>
        [JsonIgnore]
        public bool TipPaymentGatewaySpecified { get; set; }

        public decimal? TotalDiscount { get; set; }

        public string VariantInventoryManagement { get; set; }

        public bool? ProductExists { get; set; }

        public decimal? PreTaxPrice { get; set; }

        public long? FulfillmentLineItemId { get; set; }
    }
}