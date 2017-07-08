namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    public class HomeController : OperatorBaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}