using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class ProductVariant : BaseEntity
    {
        public Guid ProductId { get; set; }

        /// <summary>
        /// The unique numeric identifier for the product.
        /// </summary>
        public long? ShopifyProductId { get; set; }

        /// <summary>
        /// The title of the product variant.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A unique identifier for the product in the shop.
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// The order of the product variant in the list of product variants. 1 is the first position.
        /// </summary>
        public int? Position { get; set; }

        /// <summary>
        /// The weight of the product variant in grams.
        /// </summary>
        public long? Grams { get; set; }

        /// <summary>
        /// Specifies whether or not customers are allowed to place an order for a product variant when it's out of stock. Known values are 'deny' and 'continue'.
        /// </summary>
        public string InventoryPolicy { get; set; }

        /// <summary>
        /// Service that is doing the fulfillment. Can be 'manual' or any custom string.
        /// </summary>
        public string FulfillmentService { get; set; }

        /// <summary>
        /// The unique identifier for the inventory item, which is used in the Inventory API to query for inventory information.
        /// </summary>
        public long? InventoryItemId { get; set; }

        /// <summary>
        /// Specifies whether or not Shopify tracks the number of items in stock for this product variant. Known values are 'blank' and 'shopify'.
        /// </summary>
        public string InventoryManagement { get; set; }

        /// <summary>
        /// The price of the product variant.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// The competitors prices for the same item.
        /// </summary>
        public decimal? CompareAtPrice { get; set; }

        /// <summary>
        /// Custom properties that a shop owner can use to define product variants.
        /// </summary>
        public string Option1 { get; set; }

        /// <summary>
        /// Custom properties that a shop owner can use to define product variants.
        /// </summary>
        public string Option2 { get; set; }

        /// <summary>
        /// Custom properties that a shop owner can use to define product variants.
        /// </summary>
        public string Option3 { get; set; }


        /// <summary>
        /// Specifies whether or not a tax is charged when the product variant is sold.
        /// </summary>
        public bool? Taxable { get; set; }

        /// <summary>
        /// Specifies a tax code which is used for Avalara tax integrations
        /// </summary>
        public string TaxCode { get; set; }

        /// <summary>
        /// Specifies whether or not a customer needs to provide a shipping address when placing an order for this product variant.
        /// </summary>
        public bool? RequiresShipping { get; set; }

        /// <summary>
        /// The barcode, UPC or ISBN number for the product.
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// The number of items in stock for this product variant.
        /// NOTE: After 2018-07-01, this field will be read-only in the Shopify API. Use the `InventoryLevelService` instead.
        /// </summary>
        public long? InventoryQuantity { get; set; }

        /// <summary>
        /// The unique numeric identifier for one of the product's images.
        /// </summary>
        public long? ImageId { get; set; }

        /// <summary>
        /// The weight of the product variant in the unit system specified with weight_unit.
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// The unit system that the product variant's weight is measure in. The weight_unit can be either "g", "kg, "oz", or "lb".
        /// </summary>
        public string WeightUnit { get; set; }

        /// <summary>
        /// Additional metadata about the <see cref="ProductVariant"/>. Note: This is not naturally returned with a <see cref="ProductVariant"/> response, as
        /// Shopify will not return <see cref="ProductVariant"/> metafields unless specified. Instead, you need to query metafields with <see cref="MetaFieldService"/>.
        /// Uses include: Creating, updating, & deserializing webhook bodies that include them.
        /// </summary>
        //public IEnumerable<MetaField> Metafields { get; set; }

        /// <summary>
        /// A list of the variant's presentment prices and compare-at prices in each of the shop's enabled presentment currencies
        /// </summary>
        //public IEnumerable<PresentmentPrice> PresentmentPrices { get; set; }
    }
}
