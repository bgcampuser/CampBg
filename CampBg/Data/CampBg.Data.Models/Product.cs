namespace CampBg.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using CampBg.Data.Contracts;

    public class Product : DeletableEntity
    {
        public Product()
        {
            this.ProductImages = new HashSet<ProductImage>();
            this.PropertyValues = new HashSet<PropertyValue>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string NameEn { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string DescriptionEn { get; set; }

        public int ManufacturerId { get; set; }

        public string ManufacturerIdentificationNumber { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }

        public int? SubcategoryId { get; set; }

        [ForeignKey("SubcategoryId")]
        public Subcategory Subcategory { get; set; }

        public int? SubcategoryOptionId { get; set; }

        [ForeignKey("SubcategoryOptionId")]
        public SubcategoryOption SubcategoryOption { get; set; }

        public bool IsPopular { get; set; }

        [NotMapped]
        public ProductImage DefaultImage
        {
            get
            {
                return this.ProductImages.FirstOrDefault(img => img.IsDefault);
            }
        }

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ICollection<PropertyValue> PropertyValues { get; set; }

        public virtual ICollection<Product> RelatedProducts { get; set; }
    }
}