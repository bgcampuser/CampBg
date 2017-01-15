namespace CampBg.Data.Models
{
    using System.Collections.Generic;

    using CampBg.Data.Contracts;

    public class Category : DeletableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameEn { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
