namespace CampBg.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CampBg.Data.Contracts;

    public class DeliveryAddress : DeletableEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public UserProfile UserProfile { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; }

        [Range(0, int.MaxValue)]
        public int PostalCode { get; set; }
    }
}