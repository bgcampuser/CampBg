namespace CampBg.Web.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class StaticPageHomeViewModel
    {
        public static Expression<Func<StaticPage, StaticPageHomeViewModel>> FromStaticPageBg
        {
            get
            {
                return
                    page =>
                    new StaticPageHomeViewModel
                        {
                            Id = page.Id, Title = page.TitleBg, Address = page.AddressBarName
                        };
            }
        }

        public static Expression<Func<StaticPage, StaticPageHomeViewModel>> FromStaticPageEn
        {
            get
            {
                return
                    page =>
                    new StaticPageHomeViewModel
                        {
                            Id = page.Id, Title = page.TitleEn, Address = page.AddressBarName
                        };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }
    }
}