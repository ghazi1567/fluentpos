using FluentPOS.Shared.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{
    public class Warehouse : BaseEntity
    {
        /// <summary>
        /// Whether the location is active.
        /// </summary>
        public bool? Active { get; set; }

        /// <summary>
        /// The name of the location.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// The zip or postal code.
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The city the location is in.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The province the location is in.
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// The code of the province the location is in.
        /// </summary>
        public string ProvinceCode { get; set; }

        /// <summary>
        /// The country the location is in.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The name of the country the location is in.
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// The code of the country the location is in.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The phone number of the location. Can contain special chars like - and +.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Whether this is a fulfillment service location. If true, then the location is a fulfillment service location. If false, then the location was created by the merchant and isn't tied to a fulfillment service.
        /// </summary>
        public bool? Legacy { get; set; }

        /// <summary>
        /// The localized name of the location's country.
        /// </summary>
        public string LocalizedCountryName { get; set; }

        /// <summary>
        /// The localized name of the location's region. Typically a province, state, or prefecture.
        /// </summary>
        public string LocalizedProvinceName { get; set; }

        public long? ParentId { get; set; }

        public string Code { get; set; }

        public bool Default { get; set; }

        public int Position { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string PickupAddress { get; set; }

        public string PickupAddressCode { get; set; }

        public string PostexToken { get; set; }

        public string PostexUrl { get; set; }
    }
}