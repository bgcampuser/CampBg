namespace CampBg.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CampBg.Data.Contracts;

    public class StaticPageCategory : DeletableEntity
    {
        public StaticPageCategory()
        {
            this.Pages = new HashSet<StaticPage>();
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string NameBg { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string NameEn { get; set; }

        public virtual ICollection<StaticPage> Pages { get; set; }
    }
}