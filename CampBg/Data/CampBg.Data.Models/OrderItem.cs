namespace CampBg.Data.Models
{
    using System.Collections.Generic;

    using CampBg.Data.Contracts;

    public class OrderItem : DeletableEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual ICollection<PropertyValue> PropertyValues { get; set; }
    }
}