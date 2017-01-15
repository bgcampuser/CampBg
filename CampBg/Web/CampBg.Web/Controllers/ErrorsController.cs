namespace CampBg.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorsController : BaseController
    {
        public ActionResult NotFound()
        {
            return this.View();
        }
    }
}