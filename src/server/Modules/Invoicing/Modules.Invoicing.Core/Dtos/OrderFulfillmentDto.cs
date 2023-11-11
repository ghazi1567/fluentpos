﻿using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.DTOs.Dtos;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class OrderFulfillmentDto : BaseEntityDto
    {

        /// <summary>
        /// A historical record of each item in the fulfillment.
        /// </summary>
        public IEnumerable<OrderLineItemDto> LineItems { get; set; }

        /// <summary>
        /// The unique numeric identifier for the order.
        /// </summary>
        public long? OrderId { get; set; }

        /// <summary>
        /// A textfield with information about the receipt.
        /// </summary>
        public object Receipt { get; set; }

        /// <summary>
        /// The status of the fulfillment. Valid values are 'pending', 'open', 'success', 'cancelled',
        /// 'error' and 'failure'.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The unique identifier of the location that the fulfillment should be processed for.
        /// </summary>
        public long? LocationId { get; set; }


        /// <summary>
        /// This property is undocumented by Shopify. It appears to be the customer's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A flag indicating whether the customer should be notified. If set to true, an email will be
        /// sent when the fulfillment is created or updated. The default value is false for fulfillments
        /// on any orders created initially through the API. For all other orders, the default value is true.
        /// </summary>
        public bool? NotifyCustomer { get; set; }


        /// <summary>
        /// This property is undocumented by Shopify. It appears to be the shipping address of the order
        /// </summary>
        //public Address Destination { get; set; }

        /// <summary>
        /// The name of the shipping company.
        /// </summary>
        public string TrackingCompany { get; set; }

        /// <summary>
        /// The shipping number, provided by the shipping company. If multiple tracking numbers
        /// exist (<see cref="TrackingNumbers"/>), returns the first number.
        /// </summary>
        public string TrackingNumber { get; set; }

        /// <summary>
        /// A list of shipping numbers, provided by the shipping company. May be null.
        /// </summary>
        public IEnumerable<string> TrackingNumbers { get; set; }

        /// <summary>
        /// The tracking url, provided by the shipping company. May be null. If multiple tracking URLs
        /// exist (<see cref="TrackingUrls"/>), returns the first URL.
        /// </summary>
        public string TrackingUrl { get; set; }

        /// <summary>
        /// An array of one or more tracking urls, provided by the shipping company. May be null.
        /// </summary>
        public IEnumerable<string> TrackingUrls { get; set; }

        /// <summary>
        /// The date and time when the fulfillment was last modified.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// States the name of the inventory management service.
        /// </summary>
        public string VariantInventoryManagement { get; set; }

        /// <summary>
        /// This property is undocumented by Shopify.
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// This property is undocumented by Shopify.
        /// </summary>
        public string ShipmentStatus { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// The address of the fulfillment location.
        /// </summary>
        //[JsonProperty("origin_address")]
        //public FulfillmentOriginAddress OriginAddress { get; set; }
    }
}