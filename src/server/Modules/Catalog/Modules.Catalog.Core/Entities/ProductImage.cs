using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class ProductImage : BaseEntity
    {
        /// <summary>
        /// The id of the product associated with the image.
        /// </summary>
        public long? ProductId { get; set; }

        /// <summary>
        /// The order of the product image in the list. The first product image is at position 1 and is the "main" image for the product.
        /// </summary>
        public int? Position { get; set; }


        /// <summary>
        /// Specifies the location of the product image.
        /// </summary>
        public string Src { get; set; }

        /// <summary>
        /// Specifies the file name of the image when creating a <see cref="ProductImage"/>, where it's then converted into the <see cref="Src"/> path
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// A base64 image attachment. Only used when creating a <see cref="ProductImage"/>, where it's then converted into the <see cref="Src"/>.
        /// </summary>
        public string Attachment { get; set; }

        /// <summary>
        /// An array of variant ids associated with the image.
        /// </summary>
        // public IEnumerable<long> VariantIds { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        public string Alt { get; set; }

        /// <summary>
        /// Additional metadata about the <see cref="ProductImage"/>. Note: This is not naturally returned with a <see cref="ProductImage"/> response, as
        /// Shopify will not return <see cref="ProductImage"/> metafields unless specified. Instead, you need to query metafields with <see cref="MetaFieldService"/>. 
        /// Uses include: Creating, updating, & deserializing webhook bodies that include them.
        /// </summary>
        // public IEnumerable<MetaField> Metafields { get; set; }
    }
}
