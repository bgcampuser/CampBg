namespace CampBg.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using CampBg.Data.Contracts;

    public class Subcategory : DeletableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameEn { get; set; }

        public bool IsPopular { get; set; }

        public ICollection<Property> Properties { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public ICollection<SubcategoryOption> SubcategoryOptions { get; set; }
    }
}