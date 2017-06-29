namespace CampBg.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using CampBg.Data.Contracts;

    public class PropertyValue : IDeletable, IAuditable
    {
        public PropertyValue()
        {
            this.OrderItems = new HashSet<OrderItem>();
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public string Value { get; set; }

        public string ValueEn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
