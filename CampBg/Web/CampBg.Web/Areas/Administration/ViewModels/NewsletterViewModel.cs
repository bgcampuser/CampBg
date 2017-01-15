namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using CampBg.Data.Models;

    public class NewsletterViewModel
    {
        public static Expression<Func<Newsletter, NewsletterViewModel>> FromNewsletter
        {
            get
            {
                return n => new NewsletterViewModel
                                {
                                    Id = n.Id,
                                    Title = n.Title,
                                    Contents = n.Contents,
                                    AlreadySent = n.AlreadySent
                                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Html)]
        [AllowHtml]
        [Display(Name = "Съдържание на български")]
        [Required(AllowEmptyStrings = false)]
        public string Contents { get; set; }

        public bool AlreadySent { get; set; }
    }
}