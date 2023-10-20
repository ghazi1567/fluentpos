using FluentPOS.Shared.DTOs.Dtos;

namespace FluentPOS.Modules.Invoicing.Core.Dtos
{
    public class InternalAddressDto : BaseEntityDto
    {
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
        /// The company.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The two-letter country code corresponding to the country.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The normalized country name.
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Indicates whether this address is the default address.
        /// </summary>
        public bool? Default { get; set; }

        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The latitude. Auto-populated by Shopify on the order's Billing/Shipping address.
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// The longitude. Auto-populated by Shopify on the order's Billing/Shipping address.
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// The name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The phone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The province or state name
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// The two-letter province or state code.
        /// </summary>
        public string ProvinceCode { get; set; }

        /// <summary>
        /// The ZIP or postal code.
        /// </summary>
        public string Zip { get; set; }
    }
}
