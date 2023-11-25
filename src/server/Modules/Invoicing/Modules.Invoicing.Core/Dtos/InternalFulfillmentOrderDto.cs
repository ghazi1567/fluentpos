using FluentPOS.Shared.DTOs.Dtos;
using FluentPOS.Shared.DTOs.Sales.Enums;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class InternalFulfillmentOrderDto : OrderPricingDto
    {
        /// <summary>
        /// The ID of the shop that's associated with the fulfillment order.
        /// </summary>
        public long? ShopId { get; set; }

        /// <summary>
        /// The ID of the order that's associated with the fulfillment order.
        /// </summary>
        public long? OrderId { get; set; }

        public Guid InternalOrderId { get; set; }

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
        public InternalAddressDto FulfillmentOrderDestination { get; set; }

        /// <summary>
        /// Represents line items belonging to a fulfillment order:
        /// </summary>
        public IEnumerable<InternalFulfillmentOrderLineItemDto> FulfillmentOrderLineItems { get; set; }

        /// <summary>
        /// The datetime (in UTC) when the fulfillment order is ready for fulfillment. When this datetime is reached, a scheduled fulfillment
        /// order is automatically transitioned to open. For more information about fulfillment statuses, refer to the status property.
        /// </summary>
        public DateTimeOffset? FulfillAt { get; set; }

        /// <summary>
        /// The latest date and time by which all items in the fulfillment order need to be fulfilled.
        /// </summary>
        public DateTimeOffset? FulfilledBy { get; set; }

        public Guid? WarehouseId { get; set; }

        public long? TotalQuantity { get; set; }

        public OrderType OrderType { get; set; }

        /// <summary>
        /// The customer's spllited order name as represented by a number, e.g. '#1001'.
        /// </summary>
        public string Name { get; set; }

        public Guid? StockId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string TrackingNumber { get; set; }

        public string TrackingStatus { get; set; }

        public string TrackingUrl { get; set; }

        public string TrackingCompany { get; set; }
    }
}