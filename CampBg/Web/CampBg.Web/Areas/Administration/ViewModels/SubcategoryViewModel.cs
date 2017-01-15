namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class SubcategoryViewModel
    {
        public static Expression<Func<Subcategory, SubcategoryViewModel>> FromSubcategory
        {
            get
            {
                return
                    propertyCategory => new SubcategoryViewModel
                    {
                        Id = propertyCategory.Id,
                        Name = propertyCategory.Name,
                        CategoryId = propertyCategory.CategoryId,
                        IsPopular = propertyCategory.IsPopular,
                        NameEn = propertyCategory.NameEn
                    };
            }
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string NameEn { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public bool IsPopular { get; set; }
    }
}