using FluentPOS.Shared.DTOs.Sales.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    [Table("FulfillmentOrders")]
    public class InternalFulfillmentOrder : OrderPricing
    {
        /// <summary>
        /// The ID of the shop that's associated with the fulfillment order.
        /// </summary>
        public long? ShopId { get; set; }

        /// <summary>
        /// The ID of the order that's associated with the fulfillment order.
        /// </summary>
        public long? OrderId { get; set; }

        public long InternalOrderId { get; set; }


        /// <summary>
        /// The ID of the location that has been assigned to do the work.
        /// </summary>
        public long? AssignedLocationId { get; set; }

        /// <summary>
        /// The status of the fulfillment order.
        /// </summary>
        public string RequestStatus { get; set; }

        /// <summary>
        /// "open".
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Destination for the fulfillment.
        /// </summary>
        public InternalAddress FulfillmentOrderDestination { get; set; }

        /// <summary>
        /// Represents line items belonging to a fulfillment order:
        /// </summary>
        public IEnumerable<InternalFulfillmentOrderLineItem> FulfillmentOrderLineItems { get; set; }

        /// <summary>
        /// The datetime (in UTC) when the fulfillment order is ready for fulfillment. When this datetime is reached, a scheduled fulfillment
        /// order is automatically transitioned to open. For more information about fulfillment statuses, refer to the status property.
        /// </summary>
        public DateTimeOffset? FulfillAt { get; set; }

        /// <summary>
        /// The latest date and time by which all items in the fulfillment order need to be fulfilled.
        /// </summary>
        public DateTimeOffset? FulfilledBy { get; set; }

        public long? WarehouseId { get; set; }

        public long? TotalQuantity { get; set; }

        public OrderType OrderType { get; set; }

        /// <summary>
        /// The customer's spllited order name as represented by a number, e.g. '#1001'.
        /// </summary>
        public string Name { get; set; }

        public long? StockId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string TrackingNumber { get; set; }

        public string TrackingStatus { get; set; }

        public string TrackingUrl { get; set; }

        public string TrackingCompany { get; set; }

        public void SetOrderStatus(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
        }

        public void SetOrderName(string orderName)
        {
            Name = orderName;
        }

        public void SetOrderType(OrderType orderType)
        {
            OrderType = orderType;
        }

        public void SetTotalQuantity()
        {
            TotalQuantity = FulfillmentOrderLineItems.Sum(x => x.Quantity);
        }

        public void SetLineItemPrice(Dictionary<long?, decimal?> skuQty)
        {
            foreach (var lineItem in FulfillmentOrderLineItems)
            {
                lineItem.Price = skuQty[lineItem.VariantId];
            }
        }

        public void SetShippingPrice(decimal? shippingCost, decimal? singleItemShippingCost)
        {
            if (singleItemShippingCost.HasValue)
            {
                TotalShippingPrice = TotalQuantity * singleItemShippingCost.Value;
            }
            else
            {
                TotalShippingPrice = shippingCost;
            }
        }

        public void SetDiscountAmount(decimal? discountAmount, decimal? singleItemDiscount)
        {
            if (singleItemDiscount.HasValue)
            {
                TotalDiscounts = TotalQuantity * singleItemDiscount.Value;
            }
            else
            {
                TotalDiscounts = discountAmount;
            }
        }

        public void SetTaxAmount(decimal? taxAmount, decimal? singleItemTax)
        {
            if (singleItemTax.HasValue)
            {
                TotalTax = TotalQuantity * singleItemTax.Value;
            }
            else
            {
                TotalTax = taxAmount;
            }
        }

        public void CalculateTotal()
        {
            TotalLineItemsPrice = FulfillmentOrderLineItems.Sum(x => x.Quantity * x.Price);
            SubtotalPrice = TotalLineItemsPrice - TotalDiscounts;
            TotalPrice = SubtotalPrice + TotalShippingPrice + TotalTax;
        }
    }
}