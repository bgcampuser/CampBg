namespace CampBg.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CampBg.Data.Contracts;

    public class StaticPage : DeletableEntity
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string AddressBarName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string TitleBg { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string TitleEn { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ContentBg { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ContentEn { get; set; }

        [ForeignKey("CategoryId")]
        public virtual StaticPageCategory Category { get; set; }

        public int CategoryId { get; set; }
    }
}
