namespace CampBg.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using CampBg.Data.Contracts;

    public class SubcategoryOption : IDeletable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameEn { get; set; }

        public int SubcategoryId { get; set; }

        [ForeignKey("SubcategoryId")]
        public Subcategory Subcategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public System.DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
