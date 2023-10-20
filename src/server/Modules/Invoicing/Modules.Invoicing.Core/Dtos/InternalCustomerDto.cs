using FluentPOS.Shared.DTOs.Dtos;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class InternalCustomerDto : BaseEntityDto
    {
        /// <summary>
        /// A list of addresses for the customer.
        /// </summary>
        public IEnumerable<InternalAddressDto> Addresses { get; set; }


        /// <summary>
        /// Currency used for customer's last order
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The default address for the customer.
        /// </summary>
        public InternalAddressDto DefaultAddress { get; set; }

        /// <summary>
        /// The email address of the customer.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The customer's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The customer's identifier used with Multipass login
        /// </summary>
        public string MultipassIdentifier { get; set; }

        /// <summary>
        /// The customer's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The id of the customer's last order. 
        /// **Note**: this value is deprecated specifically when the customer is returned using the Orders API. In that case, the value will always be null. The property is still available via the Customers API.
        /// </summary>
        /// <remarks>Property can be null or longer than max int32 value. Set to nullable long instead.</remarks>
        public long? LastOrderId { get; set; }

        /// <summary>
        /// The name of the customer's last order. This is directly related to the Order's name field.
        /// **Note**: this value is deprecated specifically when the customer is returned using the Orders API. In that case, the value will always be null. The property is still available via the Customers API.
        /// </summary>
        public string LastOrderName { get; set; }

        /// <summary>
        /// A note about the customer.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// The number of orders associated with this customer.
        /// **Note**: this value is deprecated specifically when the customer is returned using the Orders API. In that case, the value will always be null. The property is still available via the Customers API.
        /// </summary>
        public int? OrdersCount { get; set; }

        /// <summary>
        /// The phone number for the customer. Valid formats can be of different types, for example:
        /// 
        /// 6135551212
        /// 
        /// +16135551212
        /// 
        /// 555-1212
        /// 
        /// (613)555-1212
        /// 
        /// +1 613-555-1212
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The state of the customer in a shop. Valid values are 'disabled', 'decline', 'invited' and 'enabled'.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Tags are additional short descriptors formatted as a string of comma-separated values.
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Indicates whether the customer should be charged taxes when placing orders. 
        /// </summary>
        public bool? TaxExempt { get; set; }

        /// <summary>
        /// Whether the customer is exempt from paying specific taxes on their order. Canadian taxes only
        /// </summary>
        public string[] TaxExemptions { get; set; }

        /// <summary>
        /// The total amount of money that the customer has spent at the shop.
        /// **Note**: this value is deprecated specifically when the customer is returned using the Orders API. In that case, the value will always be null. The property is still available via the Customers API.
        /// </summary>
        /// <remarks>The Shopify API actually returns this value as a string, but Json.Net can automatically convert to decimal.</remarks>
        public decimal? TotalSpent { get; set; }

        /// <summary>
        /// States whether or not the email address has been verified.
        /// </summary>
        public bool? VerifiedEmail { get; set; }
    }
}