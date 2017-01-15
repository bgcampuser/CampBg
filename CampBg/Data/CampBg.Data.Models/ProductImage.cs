namespace CampBg.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using CampBg.Data.Contracts;

    public class ProductImage : DeletableEntity
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public string ThumbnailLocation { get; set; }

        public bool IsDefault { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }
    }
}