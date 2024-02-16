using FluentPOS.Shared.DTOs.Sales.Enums;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class OrderSummaryResponseDto
    {
        public OrderStatus Status { get; set; }

        public OrderType OrderType { get; set; }

        public long? WarehouseId { get; set; }

        public string OrderNumber { get; set; }

        public string PaymentMethod { get; set; }


        /// <summary>
        /// The mailing address.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// An additional field for the mailing address.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// The city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The phone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The province or state name
        /// </summary>
        public string Province { get; set; }

        public string TrackingNumber { get; set; }

        public string TrackingStatus { get; set; }

        public string TrackingUrl { get; set; }

        public string TrackingCompany { get; set; }

        public long LineitemCount { get; set; }

        public long InternalFulFillmentOrderId { get; set; }

        public long FulFillmentOrderId { get; set; }

        public long InternalOrderId { get; set; }

        public long OrderId { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}
