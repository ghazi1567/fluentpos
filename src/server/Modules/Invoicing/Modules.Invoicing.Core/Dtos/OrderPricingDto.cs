using FluentPOS.Shared.DTOs.Dtos;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class OrderPricingDto : BaseEntityDto
    {
        /// <summary>
        /// The sum of all the prices of all the items in the order.
        /// </summary>
        public decimal? TotalLineItemsPrice { get; set; }

        /// <summary>
        /// The current total discounts on the order in the shop currency. The value of this field reflects order edits, returns, and refunds.
        /// </summary>
        public decimal? CurrentTotalDiscounts { get; set; }

        /// <summary>
        /// Price of the order before shipping and taxes
        /// </summary>
        public decimal? SubtotalPrice { get; set; }

        public decimal? TotalShippingPrice { get; set; }

        /// <summary>
        /// The sum of all the taxes applied to the order (must be positive).
        /// </summary>
        public decimal? TotalTax { get; set; }

        /// <summary>
        /// The sum of all the prices of all the items in the order, with taxes and discounts included (must be positive).
        /// </summary>
        public decimal? TotalPrice { get; set; }

        /// <summary>
        /// The total outstanding amount of the order in the shop currency.
        /// </summary>
        public string TotalOutstanding { get; set; }
    }
}
