namespace CampBg.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CampBg.Data.Contracts;

    public class Order : DeletableEntity
    {
        public int Id { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public string AdditionalDetails { get; set; }

        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public UserProfile User { get; set; }

        public bool IsPaid { get; set; }

        public bool IsFinalized { get; set; }

        public bool IsOrdered { get; set; }

        public int DeliveryAddressId { get; set; }

        [ForeignKey("DeliveryAddressId")]
        public virtual DeliveryAddress DeliveryAddress { get; set; }

        [Required]
        public virtual PaymentMethod PaymentMethod { get; set; }

        public string EmailAddress { get; set; }

        public string OrderedBy { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string EIK { get; set; }

        public string VATNumber { get; set; }

        public string Custodian { get; set; }
    }
}