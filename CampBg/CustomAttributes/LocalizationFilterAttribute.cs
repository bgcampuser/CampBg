namespace CustomAttributes
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    public class LocalizationFilterAttribute : ActionFilterAttribute
    {
        private readonly string defaultCulture;

        public LocalizationFilterAttribute(string defaultCulture = "en-GB")
        {
            this.defaultCulture = defaultCulture;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var locales = new Dictionary<string, string> { { "bg", "bg-BG" }, { "en", "en-GB" } };

            var subdomain = this.GetSubDomain();

            if (subdomain != string.Empty && locales.ContainsKey(subdomain))
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(locales[subdomain]);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(locales[subdomain]);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(this.defaultCulture);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(this.defaultCulture);
            }

            base.OnActionExecuting(filterContext);
        }

        private string GetSubDomain()
        {
            var url = HttpContext.Current.Request.Headers["HOST"];
            var index = url.IndexOf(".", System.StringComparison.Ordinal);

            if (index < 0)
            {
                return string.Empty;
            }

            var subdomain = url.Split('.')[0];
            if (subdomain == "www" || subdomain == "localhost")
            {
                return string.Empty;
            }

            return subdomain;
        }
    }
}
