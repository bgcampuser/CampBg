namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class StaticPageCategoryViewModel
    {
        public static Expression<Func<StaticPageCategory, StaticPageCategoryViewModel>> FromStaticPageCategory
        {
            get
            {
                return cat => new StaticPageCategoryViewModel
                                  {
                                      Id = cat.Id,
                                      NameBg = cat.NameBg,
                                      NameEn = cat.NameEn
                                  };
            }
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string NameBg { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string NameEn { get; set; }
    }
}