namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using CampBg.Web.Controllers;

    [Authorize(Roles = "Administrator")]
    public class AdministrationBaseController : BaseController
    {
    }
}