namespace CampBg.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class StaticPageViewModel
    {
        public static Expression<Func<StaticPage, StaticPageViewModel>> FromStaticPageBg
        {
            get
            {
                return
                    page =>
                    new StaticPageViewModel
                        {
                            Id = page.Id,
                            AddressBarName = page.AddressBarName,
                            Title = page.TitleBg,
                            Content = page.ContentBg
                        };
            }
        }

        public static Expression<Func<StaticPage, StaticPageViewModel>> FromStaticPageEn
        {
            get
            {
                return
                    page =>
                    new StaticPageViewModel
                        {
                            Id = page.Id,
                            AddressBarName = page.AddressBarName,
                            Title = page.TitleEn,
                            Content = page.ContentEn
                        };
            }
        }

        public int Id { get; set; }

        public string AddressBarName { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }
    }
}