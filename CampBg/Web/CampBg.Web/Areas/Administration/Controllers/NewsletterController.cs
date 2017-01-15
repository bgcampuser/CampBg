namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using CampBg.Data.Models;
    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using OJS.Common;

    public class NewsletterController : AdministrationBaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var newsletters = this.Data.Newsletters.All().Select(NewsletterViewModel.FromNewsletter);

            return this.Json(newsletters.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult Create([DataSourceRequest]DataSourceRequest request, NewsletterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var newsletter = new Newsletter
                                     {
                                         Title = model.Title,
                                         Contents = model.Contents
                                     };

                this.Data.Newsletters.Add(newsletter);
                this.Data.SaveChanges();
                model.Id = newsletter.Id;

                return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
            }

            return null;
        }

        public ActionResult Destroy(NewsletterViewModel model)
        {
            var newsletterToDelete = this.Data.Newsletters
                .GetById(model.Id);

            if (newsletterToDelete != null)
            {
                this.Data.Newsletters.Delete(newsletterToDelete);
                this.Data.SaveChanges();
            }

            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(NewsletterViewModel model)
        {
            var newsletterToUpdate = this.Data.Newsletters.GetById(model.Id);

            if (this.ModelState.IsValid)
            {
                newsletterToUpdate.Title = model.Title;
                newsletterToUpdate.Contents = model.Contents;
                newsletterToUpdate.AlreadySent = model.AlreadySent;

                this.Data.SaveChanges();
            }

            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendNewsletter(int id)
        {
            var newsletter = this.Data.Newsletters.GetById(id);
            var users = this.Data.Users.All().AsQueryable().Where(x => x.IsSubscribedForNewsletter).ToList();
            var sender = this.MailSender;

            for (int i = 0; i < users.Count; i += 50)
            {
                var usersToReceive = users.Take(50).Skip(i).Select(x => x.Email);
                sender.SendMail("dininski@gmail.com", newsletter.Title, newsletter.Contents, usersToReceive);
            }

            return this.Json("Done", JsonRequestBehavior.AllowGet);
        }
    }
}