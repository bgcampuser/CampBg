namespace CampBg.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class StaticPageCategoryHomeViewModel
    {
        public StaticPageCategoryHomeViewModel()
        {
            this.StaticPages = new HashSet<StaticPageHomeViewModel>();
        }

        public static Expression<Func<StaticPageCategory, StaticPageCategoryHomeViewModel>> FromStaticPageCategoryBg
        {
            get
            {
                return
                    cat =>
                    new StaticPageCategoryHomeViewModel
                        {
                            Id = cat.Id,
                            Name = cat.NameBg,
                            StaticPages =
                                cat.Pages.Where(x => !x.IsDeleted)
                                .AsQueryable()
                                .Select(StaticPageHomeViewModel.FromStaticPageBg)
                        };
            }
        }

        public static Expression<Func<StaticPageCategory, StaticPageCategoryHomeViewModel>> FromStaticPageCategoryEn
        {
            get
            {
                return
                    cat =>
                    new StaticPageCategoryHomeViewModel
                    {
                        Id = cat.Id,
                        Name = cat.NameEn,
                        StaticPages =
                            cat.Pages.Where(x => !x.IsDeleted)
                            .AsQueryable()
                            .Select(StaticPageHomeViewModel.FromStaticPageEn)
                    };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<StaticPageHomeViewModel> StaticPages { get; set; }
    }
}