namespace CampBg.Web.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class SubcategoryOptionViewModel
    {
        public static Expression<Func<SubcategoryOption, SubcategoryOptionViewModel>> FromSubcategoryOption
        {
            get
            {
                return subOpt => new SubcategoryOptionViewModel
                                   {
                                       Id = subOpt.Id,
                                       SubcategoryId = subOpt.SubcategoryId,
                                       Name = subOpt.Name
                                   };
            }
        }

        public static Expression<Func<SubcategoryOption, SubcategoryOptionViewModel>> FromSubcategoryOptionEn
        {
            get
            {
                return
                    subOpt =>
                    new SubcategoryOptionViewModel
                        {
                            Id = subOpt.Id,
                            SubcategoryId = subOpt.SubcategoryId,
                            Name = subOpt.NameEn
                        };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int SubcategoryId { get; set; }
    }
}