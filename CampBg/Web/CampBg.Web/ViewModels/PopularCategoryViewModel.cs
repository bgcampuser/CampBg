namespace CampBg.Web.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class PopularCategoryViewModel
    {
        public static Expression<Func<Subcategory, PopularCategoryViewModel>> FromSubcategoryEn
        {
            get
            {
                return sc => new PopularCategoryViewModel
                                 {
                                     CategoryName = sc.Category.NameEn,
                                     SubcategoryName = sc.NameEn,
                                     CategoryNameUrl = sc.Category.NameEn,
                                     SubcategoryNameUrl = sc.NameEn
                                 };
            }
        }

        public static Expression<Func<Subcategory, PopularCategoryViewModel>> FromSubcategoryBg
        {
            get
            {
                return sc => new PopularCategoryViewModel
                                 {
                                     CategoryName = sc.Category.Name,
                                     SubcategoryName = sc.Name,
                                     CategoryNameUrl = sc.Category.NameEn,
                                     SubcategoryNameUrl = sc.NameEn
                                 };
            }
        }

        public string CategoryName { get; set; }

        public string SubcategoryName { get; set; }

        public string CategoryNameUrl { get; set; }

        public string SubcategoryNameUrl { get; set; }
    }
}