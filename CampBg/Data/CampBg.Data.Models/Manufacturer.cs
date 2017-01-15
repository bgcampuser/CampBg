namespace CampBg.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CampBg.Data.Contracts;

    public class Manufacturer : IDeletable
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string Logo { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Product> Products { get; set; }

        public bool IsInTopMenu { get; set; }
    }
}
