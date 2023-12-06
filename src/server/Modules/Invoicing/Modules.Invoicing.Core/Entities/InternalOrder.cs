// --------------------------------------------------------------------------------------------------
// <copyright file="Order.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.DTOs.People.Customers;
using FluentPOS.Shared.DTOs.Sales.Enums;
using ShopifySharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    [Table("Orders")]
    public class InternalOrder : OrderPricing
    {
        public string ReferenceNumber { get; private set; }

        public OrderStatus Status { get; set; }

        public OrderType OrderType { get; set; }

        public void SetOrderType()
        {
            OrderType = FulfillmentOrders.Count() > 1 ? OrderType.SplittedOrder : OrderType.SingleOrder;
        }

        public DateTime TimeStamp { get; private set; }

        public Guid CustomerId { get; private set; }

        public string CustomerName { get; private set; }

        public string CustomerPhone { get; private set; }

        public string CustomerEmail { get; private set; }

        public string Note { get; private set; }

        public Guid WarehouseId { get; set; }

        public static InternalOrder InitializeOrder()
        {
            return new InternalOrder { TimeStamp = DateTime.Now };
        }

        public static InternalOrder InitializeOrder(DateTime dateTime)
        {
            return new InternalOrder { TimeStamp = dateTime };
        }

        public void AddCustomer(GetCustomerByIdResponse customer)
        {
            CustomerId = customer.Id;
            CustomerName = customer.Name;
            CustomerEmail = customer.Email;
            CustomerPhone = customer.Phone;
        }

        public void SetReferenceNumber(string referenceNumber)
        {
            ReferenceNumber = referenceNumber;
        }

        public void SetNote(string note)
        {
            Note = note;
        }

        /// <summary>
        /// Unique identifier of the app who created the order.
        /// </summary>
        public long? AppId { get; set; }

        /// <summary>
        /// The IP address of the browser used by the customer when placing the order.
        /// </summary>
        public string BrowserIp { get; set; }

        /// <summary>
        /// Indicates whether or not the person who placed the order would like to receive email updates from the shop.
        /// This is set when checking the "I want to receive occasional emails about new products, promotions and other news" checkbox during checkout.
        /// </summary>
        public bool? BuyerAcceptsMarketing { get; set; }

        /// <summary>
        /// The reason why the order was cancelled. If the order was not cancelled, this value is null. Known values are "customer", "fraud", "inventory" and "other".
        /// </summary>
        public string CancelReason { get; set; }

        /// <summary>
        /// The date and time when the order was cancelled. If the order was not cancelled, this value is null.
        /// </summary>
        public DateTimeOffset? CancelledAt { get; set; }

        /// <summary>
        /// Unique identifier for a particular cart that is attached to a particular order.
        /// </summary>
        public string CartToken { get; set; }

        /// <summary>
        /// A unique value when referencing the <see cref="ShopifySharp.Checkout"/> that's associated with the order. 
        /// </summary>
        public string CheckoutToken { get; set; }

        /// <summary>
        /// ID of the checkout that's associated with the order.
        /// </summary>
        public long? CheckoutId { get; set; }

        /// <summary>
        /// The date and time when the order was closed. If the order was not clsoed, this value is null.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; set; }

        /// <summary>
        /// Whether inventory has been reserved for the order.
        /// </summary>
        public bool? Confirmed { get; set; }

        /// <summary>
        /// The three letter code (ISO 4217) for the currency used for the payment.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The two or three letter language code, optionally followed by a region modifier. Example values could be 'en', 'en-CA', 'en-PIRATE'.
        /// </summary>
        public string CustomerLocale { get; set; }

        /// <summary>
        /// The unique numeric identifier of the POS device used.
        /// </summary>
        public long? DeviceId { get; set; }

        /// <summary>
        /// The order's email address. Note: On and after 2015-11-03, you should be using <see cref="ContactEmail"/> to refer to the customer's email address.
        /// Between 2015-11-03 and 2015-12-03, updates to an order's email will also update the customer's email. This is temporary so apps can be migrated over to
        /// doing customer updates rather than order updates to change the contact email. After 2015-12-03, updating updating an order's email will no longer update
        /// the customer's email and apps will have to use the customer update endpoint to do so.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The financial status of an order. Known values are "authorized", "paid", "pending", "partially_paid", "partially_refunded", "refunded" and "voided".
        /// </summary>
        public string FinancialStatus { get; set; }

        /// <summary>
        /// An array of <see cref="Fulfillment"/> objects for this order.
        /// </summary>
        public IEnumerable<IntenalFulfillment> Fulfillments { get; set; }

        /// <summary>
        /// The fulfillment status for this order. Known values are 'fulfilled', 'null' and 'partial'.
        /// </summary>
        public string FulfillmentStatus { get; set; }

        /// <summary>
        /// The customer's phone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Tags are additional short descriptors, commonly used for filtering and searching, formatted as a string of comma-separated values.
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// The URL for the page where the buyer landed when entering the shop.
        /// </summary>
        public string LandingSite { get; set; }

        /// <summary>
        /// An array of <see cref="LineItem"/> objects, each one containing information about an item in the order.
        /// </summary>
        public IEnumerable<InternalLineItem> LineItems { get; set; }

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
        /// The URL pointing to the order status web page. The URL will be null unless the order was created from a checkout.
        /// </summary>
        public string OrderStatusUrl { get; set; }

        /// <summary>
        /// The list of all payment gateways used for the order.
        /// </summary>
        public string PaymentGatewayNames { get; set; }

        /// <summary>
        /// The date that the order was processed at.
        /// </summary>
        public DateTimeOffset? ProcessedAt { get; set; }

        /// <summary>
        /// The type of payment processing method. Known values are 'checkout', 'direct', 'manual', 'offsite', 'express', 'free' and 'none'.
        /// </summary>
        [Obsolete("Deprecated in version 2023-04. https://shopify.dev/docs/api/release-notes/2023-04#payment-properties-on-the-order-resource-have-been-removed")]
        public string ProcessingMethod { get; set; }

        /// <summary>
        /// The website that the customer clicked on to come to the shop.
        /// </summary>
        public string ReferringSite { get; set; }


        [ForeignKey("ShippingAddress")]
        public Guid ShippingAddressId { get; set; }
        /// <summary>
        /// The mailing address to where the order will be shipped. This address is optional and will not be available on orders that do not require one.
        /// </summary>
        public InternalAddress ShippingAddress { get; set; }


        /// <summary>
        /// Where the order originated. May only be set during creation, and is not writeable thereafter.
        /// Orders created via the API may be assigned any string of your choice except for "web", "pos", "iphone", and "android".
        /// Default is "api".
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// States whether or not taxes are included in the order subtotal.
        /// </summary>
        public bool? TaxesIncluded { get; set; }

        /// <summary>
        /// States whether this is a test order.
        /// </summary>
        public bool? Test { get; set; }

        /// <summary>
        /// Unique identifier for a particular order.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The sum of all the tips in the order.
        /// </summary>
        public decimal? TotalTipReceived { get; set; }

        /// <summary>
        /// The sum of all the weights of the line items in the order, in grams.
        /// </summary>
        public long? TotalWeight { get; set; }

        /// <summary>
        /// The unique numerical identifier for the user logged into the terminal at the time the order was processed at. Only present on orders processed at point of sale.
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// The three letter code (ISO 4217) for the currency used used to display prices to the customer.
        /// </summary>
        public string PresentmentCurrency { get; set; }

        /// <summary>
        /// Indicates whether taxes on an order are estimated. Will be set to false when taxes on an order are finalized and aren't subject to any change.
        /// </summary>
        public bool? EstimatedTaxes { get; set; }

        /// <summary>
        /// The current subtotal price of the order in the shop currency. The value of this field reflects order edits, returns, and refunds.
        /// </summary>
        public decimal? CurrentSubtotalPrice { get; set; }



        /// <summary>
        /// The current total price of the order in the shop currency. The value of this field reflects order edits, returns, and refunds.
        /// </summary>
        public decimal? CurrentTotalPrice { get; set; }

        /// <summary>
        /// The current total taxes charged on the order in the shop currency. The value of this field reflects order edits, returns, or refunds.
        /// </summary>
        public decimal? CurrentTotalTax { get; set; }

        /// <summary>
        /// The purchase order number associated to this order
        /// </summary>
        public string PoNumber { get; set; }

        /// <summary>
        /// Whether this order was exempt from taxes.
        /// </summary>
        public bool? TaxExempt { get; set; }



        public DateTimeOffset? ApprovedAt { get; set; }

        public string ApprovedBy { get; set; }

        public string TrackingNumber { get; set; }

        public string TrackingStatus { get; set; }

        public string TrackingUrl { get; set; }

        public string TrackingCompany { get; set; }


        public List<InternalFulfillmentOrder> FulfillmentOrders { get; set; }

        public long? TotalQuantity { get; set; }

        public void SetTotalQuantity()
        {
            TotalQuantity = LineItems.Sum(x => x.Quantity);
        }

        public void CalculateSubTotal()
        {
            int count = 1;
            foreach (var item in FulfillmentOrders)
            {
                string orderName = OrderType == OrderType.SingleOrder ? Name : $"{Name}-S{count++}";
                item.SetOrderName(orderName);
                item.SetOrderType(OrderType);
                item.SetOrderStatus(Status);
                item.SetTotalQuantity();

                // set line item price
                var skuQty = LineItems.ToDictionary(x => x.VariantId, x => x.Price);
                item.SetLineItemPrice(skuQty);

                // set shipping cost.
                decimal? singleItemShippingCost = null;
                if (OrderType == OrderType.SplittedOrder && TotalShippingPrice.HasValue && TotalShippingPrice.Value > 0)
                {
                    singleItemShippingCost = Math.Round(TotalShippingPrice.Value / TotalQuantity.Value, 2);
                }

                item.SetShippingPrice(TotalShippingPrice, singleItemShippingCost);

                // set tax amount.
                decimal? singleItemTaxAmount = null;
                if (OrderType == OrderType.SplittedOrder && TotalTax.HasValue && TotalTax.Value > 0)
                {
                    singleItemTaxAmount = Math.Round(TotalTax.Value / TotalQuantity.Value, 2);
                }

                item.SetTaxAmount(TotalTax, singleItemTaxAmount);

                // set discount amount.
                decimal? singleItemDiscountAmount = null;
                if (OrderType == OrderType.SplittedOrder && TotalDiscounts.HasValue && TotalDiscounts.Value > 0)
                {
                    singleItemDiscountAmount = Math.Round(TotalDiscounts.Value / TotalQuantity.Value, 2);
                }

                item.SetDiscountAmount(TotalDiscounts, singleItemDiscountAmount);

                item.CalculateTotal();
            }
        }

        public void UpdateShippingAddress()
        {
            foreach (var item in FulfillmentOrders)
            {
                if (string.IsNullOrEmpty(item.FulfillmentOrderDestination.Address1))
                    item.FulfillmentOrderDestination.Address1 = ShippingAddress.Address1;
                if (string.IsNullOrEmpty(item.FulfillmentOrderDestination.Address2))
                    item.FulfillmentOrderDestination.Address2 = ShippingAddress.Address2;
                if (string.IsNullOrEmpty(item.FulfillmentOrderDestination.City))
                    item.FulfillmentOrderDestination.City = ShippingAddress.City;
                if (string.IsNullOrEmpty(item.FulfillmentOrderDestination.Country))
                    item.FulfillmentOrderDestination.Country = ShippingAddress.Country;
                if (string.IsNullOrEmpty(item.FulfillmentOrderDestination.CountryCode))
                    item.FulfillmentOrderDestination.CountryCode = ShippingAddress.CountryCode;
                if (!item.FulfillmentOrderDestination.Latitude.HasValue)
                    item.FulfillmentOrderDestination.Latitude = ShippingAddress.Latitude;
                if (!item.FulfillmentOrderDestination.Longitude.HasValue)
                    item.FulfillmentOrderDestination.Longitude = ShippingAddress.Longitude;
                if (string.IsNullOrEmpty(item.FulfillmentOrderDestination.Name))
                    item.FulfillmentOrderDestination.Name = ShippingAddress.Name;

            }
        }

        public void UpdateFulfillmentorders()
        {
            SetOrderType();
            UpdateShippingAddress();
            SetTotalQuantity();
            CalculateSubTotal();
        }
    }
}