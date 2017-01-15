namespace CampBg.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class CategoryViewModel
    {
        public static Expression<Func<Category, CategoryViewModel>> FromCategory
        {
            get
            {
                return
                    category => new CategoryViewModel
                                    {
                                        Name = category.Name,
                                        Subcategories = category.Subcategories.AsQueryable()
                                            .Where(x => !x.IsDeleted)
                                            .Select(SubcategoryViewModel.FromSubcategory)
                                    };
            }
        }

        public static Expression<Func<Category, CategoryViewModel>> FromCategoryEn
        {
            get
            {
                return
                    category => new CategoryViewModel
                    {
                        Name = category.NameEn,
                        Subcategories = category.Subcategories.AsQueryable()
                            .Where(x => !x.IsDeleted)
                            .Select(SubcategoryViewModel.FromSubcategoryEn)
                    };
            }
        }

        public string Name { get; set; }

        public IEnumerable<SubcategoryViewModel> Subcategories { get; set; }
    }
}