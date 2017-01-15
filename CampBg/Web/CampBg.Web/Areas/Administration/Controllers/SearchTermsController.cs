namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class SearchTermsController : AdministrationBaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var searchTerms = this.Data.SearchTerms.All().Select(SearchTermViewModel.FromSearchTerm);

            return this.Json(searchTerms.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}