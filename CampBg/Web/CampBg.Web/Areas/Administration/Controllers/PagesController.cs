namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using CampBg.Data.Models;
    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class PagesController : AdministrationBaseController
    {
        public ActionResult Read([DataSourceRequest]DataSourceRequest request, int id)
        {
            var pages =
                this.Data.StaticPages.All().Where(x => x.CategoryId == id).Select(StaticPageViewModel.FromStaticPage);

            return this.Json(pages.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create([DataSourceRequest]DataSourceRequest request, int categoryId, StaticPageViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var page = new StaticPage
                               {
                                   TitleBg = model.TitleBg,
                                   TitleEn = model.TitleEn,
                                   ContentBg = model.ContentBg,
                                   ContentEn = model.ContentEn,
                                   CategoryId = categoryId,
                                   AddressBarName = model.AddressBarName
                               };
                this.Data.StaticPages.Add(page);
                this.Data.SaveChanges();

                model.Id = page.Id;

                return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
            }

            return null;
        }

        public ActionResult Update([DataSourceRequest] DataSourceRequest request, StaticPageViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var page = this.Data.StaticPages.GetById(model.Id);

                page.TitleBg = model.TitleBg;
                page.TitleEn = model.TitleEn;
                page.ContentBg = model.ContentBg;
                page.ContentEn = model.ContentEn;
                page.AddressBarName = model.AddressBarName;
                               
                this.Data.SaveChanges();

                return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
            }

            return null;
        }

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, StaticPageViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.Data.StaticPages.Delete(model.Id);
                this.Data.SaveChanges();

                return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
            }

            return null;
        }
    }
}