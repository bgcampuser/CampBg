namespace CampBg.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/kendo/kendo.all.min.js",
                        "~/Scripts/kendo/kendo.aspnetmvc.min.js",
                        "~/Scripts/kendo/kendo.culture.bg-BG.min.js"));

            // TODO: Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/jquery.Jcrop.min.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/endlessScroll.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                        "~/Scripts/jquery.noty.packaged.min.js",
                        "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/default.css",
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/grid_styles").Include(
                        "~/Content/kendo/kendo.common.min.css",
                        "~/Content/kendo/kendo.black.min.css"));
        }
    }
}
