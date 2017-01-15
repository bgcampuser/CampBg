namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using CampBg.Data.Models;

    public class StaticPageViewModel
    {
        public static Expression<Func<StaticPage, StaticPageViewModel>> FromStaticPage
        {
            get
            {
                return
                    page =>
                    new StaticPageViewModel
                        {
                            Id = page.Id,
                            StaticPageCategoryId = page.CategoryId,
                            AddressBarName = page.AddressBarName,
                            TitleBg = page.TitleBg,
                            TitleEn = page.TitleEn,
                            ContentBg = page.ContentBg,
                            ContentEn = page.ContentEn
                        };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int StaticPageCategoryId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "URL Адрес")]
        public string AddressBarName { get; set; }

        [Display(Name = "Заглавие на български")]
        [Required(AllowEmptyStrings = false)]
        public string TitleBg { get; set; }

        [DataType(DataType.Html)]
        [AllowHtml]
        [Display(Name = "Съдържание на български")]
        [Required(AllowEmptyStrings = false)]
        public string ContentBg { get; set; }

        [Display(Name = "Заглавие на английски")]
        [Required(AllowEmptyStrings = false)]
        public string TitleEn { get; set; }

        [DataType(DataType.Html)]
        [AllowHtml]
        [Display(Name = "Съдържание на английски")]
        [Required(AllowEmptyStrings = false)]
        public string ContentEn { get; set; }
    }
}