using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.DTOs.Dtos;
using FluentPOS.Shared.DTOs.Sales.Enums;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class OrderResponseDto : BaseEntityDto
    {
        public OrderStatus Status { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime TimeStamp { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerEmail { get; set; }

        public string Note { get; set; }

        public Guid? WarehouseId { get; set; }

        /// <summary>
        /// The reason why the order was cancelled. If the order was not cancelled, this value is null. Known values are "customer", "fraud", "inventory" and "other".
        /// </summary>
        public string CancelReason { get; set; }

        /// <summary>
        /// The date and time when the order was cancelled. If the order was not cancelled, this value is null.
        /// </summary>
        public DateTimeOffset? CancelledAt { get; set; }

        /// <summary>
        /// The date and time when the order was closed. If the order was not clsoed, this value is null.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; set; }

        /// <summary>
        /// Whether inventory has been reserved for the order.
        /// </summary>
        public bool? Confirmed { get; set; }


        /// <summary>
        /// The order's email address. Note: On and after 2015-11-03, you should be using <see cref="ContactEmail"/> to refer to the customer's email address.
        /// Between 2015-11-03 and 2015-12-03, updates to an order's email will also update the customer's email. This is temporary so apps can be migrated over to
        /// doing customer updates rather than order updates to change the contact email. After 2015-12-03, updating updating an order's email will no longer update
        /// the customer's email and apps will have to use the customer update endpoint to do so.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The unique numeric identifier for the physical location at which the order was processed. Only present on orders processed at point of sale.
        /// </summary>
        public long? LocationId { get; set; }

        /// <summary>
        /// The customer's order name as represented by a number, e.g. '#1001'.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Numerical identifier unique to the shop. A number is sequential and starts at 1000.
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// A unique numeric identifier for the order. This one is used by the shop owner and customer.
        /// This is different from the id property, which is also a unique numeric identifier for the order, but used for API purposes.
        /// </summary>
        public int? OrderNumber { get; set; }

        /// <summary>
        /// The list of all payment gateways used for the order.
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// The date that the order was processed at.
        /// </summary>
        public DateTimeOffset? ProcessedAt { get; set; }

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

        /// <summary>
        /// Price of the order before shipping and taxes
        /// </summary>
        public decimal? SubtotalPrice { get; set; }

        /// <summary>
        /// States whether or not taxes are included in the order subtotal.
        /// </summary>
        public bool? TaxesIncluded { get; set; }


        /// <summary>
        /// The total amount of the discounts applied to the price of the order.
        /// </summary>
        public decimal? TotalDiscounts { get; set; }

        /// <summary>
        /// The sum of all the prices of all the items in the order.
        /// </summary>
        public decimal? TotalLineItemsPrice { get; set; }

        /// <summary>
        /// The sum of all the tips in the order.
        /// </summary>
        public decimal? TotalTipReceived { get; set; }

        /// <summary>
        /// The sum of all the prices of all the items in the order, with taxes and discounts included (must be positive).
        /// </summary>
        public decimal? TotalPrice { get; set; }

        /// <summary>
        /// The sum of all the taxes applied to the order (must be positive).
        /// </summary>
        public decimal? TotalTax { get; set; }

        /// <summary>
        /// The sum of all the weights of the line items in the order, in grams.
        /// </summary>
        public long? TotalWeight { get; set; }

        /// <summary>
        /// The total outstanding amount of the order in the shop currency.
        /// </summary>
        public string TotalOutstanding { get; set; }

        /// <summary>
        /// The current subtotal price of the order in the shop currency. The value of this field reflects order edits, returns, and refunds.
        /// </summary>
        public decimal? CurrentSubtotalPrice { get; set; }

        /// <summary>
        /// The current total discounts on the order in the shop currency. The value of this field reflects order edits, returns, and refunds.
        /// </summary>
        public decimal? CurrentTotalDiscounts { get; set; }

        /// <summary>
        /// The current total price of the order in the shop currency. The value of this field reflects order edits, returns, and refunds.
        /// </summary>
        public decimal? CurrentTotalPrice { get; set; }

        /// <summary>
        /// The current total taxes charged on the order in the shop currency. The value of this field reflects order edits, returns, or refunds.
        /// </summary>
        public decimal? CurrentTotalTax { get; set; }

        public decimal? TotalShippingPrice { get; set; }

        public string WarehouseName { get; set; }

        public long? AssignedLocationId { get; set; }

        public long? FulFillmentOrderId { get; set; }

        public Guid? InternalFulFillmentOrderId { get; set; }

        public string FulFillmentOrderStatus { get; set; }

        public Dictionary<long,long> VariantQty { get; set; }

        public string TrackingNumber { get; set; }

        public string TrackingStatus { get; set; }

        public string TrackingUrl { get; set; }

        public string TrackingCompany { get; set; }

        public List<InternalFulfillmentOrderLineItemDto> FulfillmentOrderLineItems { get; set; }

        public List<OrderLineItemDto> LineItems { get; set; }


    }
}