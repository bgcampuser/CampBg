namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class SubcategoryOptionViewModel
    {
        public static Expression<Func<SubcategoryOption, SubcategoryOptionViewModel>> FromSubcategoryOption
        {
            get
            {
                return
                    sub =>
                    new SubcategoryOptionViewModel
                        {
                            Id = sub.Id,
                            Name = sub.Name,
                            NameEn = sub.NameEn,
                            SubcategoryId = sub.SubcategoryId,
                        };
            }
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string NameEn { get; set; }

        [Display(Name = "Subcategory")]
        public int SubcategoryId { get; set; }
    }
}