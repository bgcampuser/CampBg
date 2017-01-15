namespace CampBg.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "StaticPages_Default",
                "Home/Page/{page}/",
                new { controller = "Home", action = "Page", page = UrlParameter.Optional },
                new[] { "CampBg.Web.Controllers" });

            routes.MapRoute(
                "Language",
                "Language/",
                new { controller = "Home", action = "Language"},
                new[] { "CampBg.Web.Controllers" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "CampBg.Web.Controllers" });
        }
    }
}
