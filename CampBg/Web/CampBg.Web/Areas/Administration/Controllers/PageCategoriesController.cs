namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class PageCategoriesController : AdministrationBaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var categories =
                this.Data.StaticPageCategories.All().Select(StaticPageCategoryViewModel.FromStaticPageCategory);

            return this.Json(categories.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }
    }
}