namespace CampBg.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class SubcategoryViewModel
    {
        public static Expression<Func<Subcategory, SubcategoryViewModel>> FromSubcategory
        {
            get
            {
                return sub => new SubcategoryViewModel
                                  {
                                      Name = sub.Name,
                                      Id = sub.Id,
                                      SubcategoryOptions = sub.SubcategoryOptions
                                                    .AsQueryable()
                                                    .Where(x => !x.IsDeleted)
                                                    .Select(SubcategoryOptionViewModel.FromSubcategoryOption)
                                  };
            }
        }

        public static Expression<Func<Subcategory, SubcategoryViewModel>> FromSubcategoryEn
        {
            get
            {
                return sub => new SubcategoryViewModel
                {
                    Name = sub.NameEn,
                    Id = sub.Id,
                    SubcategoryOptions = sub.SubcategoryOptions
                                  .AsQueryable()
                                  .Where(x => !x.IsDeleted)
                                  .Select(SubcategoryOptionViewModel.FromSubcategoryOptionEn)
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SubcategoryOptionViewModel> SubcategoryOptions { get; set; }
    }
}