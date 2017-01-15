namespace CampBg.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    using CampBg.Common.ShoppingCart.Managers;
    using CampBg.Data;
    using CampBg.Data.Models;
    using CampBg.Data.Repositories.Contracts;
    using CampBg.Extensions;
    using CampBg.Web.ViewModels;

    using OJS.Common;

    public abstract class BaseController : Controller
    {
        protected readonly IUowData Data;

        private const string DefaultCulture = "bg-BG";

        protected BaseController()
            : this(new UowData())
        {
        }

        protected BaseController(IUowData data)
        {
            this.Data = data;
            this.Locales = new Dictionary<string, string> { { "bg", "bg-BG" }, { "en", "en-GB" } };
        }

        protected UserProfile UserProfile { get; private set; }

        protected Dictionary<string, string> Locales { get; set; }
        
        protected MailSender MailSender
        {
            get
            {
                return SMTPMailSender.Instance;
            }
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            var subdomain = this.GetSubDomain(requestContext);

            var cookieCulture = requestContext.HttpContext.Request.Cookies["culture"] != null
                                    ? requestContext.HttpContext.Request.Cookies["culture"].Value
                                    : string.Empty;

            if (!string.IsNullOrEmpty(cookieCulture) && this.Locales.ContainsKey(cookieCulture))
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(this.Locales[cookieCulture]);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(this.Locales[cookieCulture]);
            }
            else if (subdomain != string.Empty && this.Locales.ContainsKey(subdomain))
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(this.Locales[subdomain]);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(this.Locales[subdomain]);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(DefaultCulture);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(DefaultCulture);
            }

            IEnumerable<StaticPageCategoryHomeViewModel> footerViewModel;

            if (Thread.CurrentThread.CurrentCulture.Name == "bg-BG")
            {
                this.ViewBag.MainMenu = new MainMenuLayoutViewModel
                                            {
                                                Categories =
                                                    this.Data.Categories.All()
                                                    .Where(x => !x.IsDeleted)
                                                    .Select(CategoryViewModel.FromCategory)
                                            };

                footerViewModel =
                    this.Data.StaticPageCategories.All()
                        .Select(StaticPageCategoryHomeViewModel.FromStaticPageCategoryBg);

                this.ViewBag.PopularCategoriesFooter =
                    this.Data.Subcategories.All()
                        .AsQueryable()
                        .Where(x => x.IsPopular)
                        .Select(PopularCategoryViewModel.FromSubcategoryBg);
            }
            else
            {
                this.ViewBag.MainMenu = new MainMenuLayoutViewModel
                {
                    Categories =
                        this.Data.Categories.All()
                        .Where(x => !x.IsDeleted)
                        .Select(CategoryViewModel.FromCategoryEn)
                };

                footerViewModel =
                    this.Data.StaticPageCategories.All()
                        .Select(StaticPageCategoryHomeViewModel.FromStaticPageCategoryEn);

                this.ViewBag.PopularCategoriesFooter =
                    this.Data.Subcategories.All()
                        .AsQueryable()
                        .Where(x => x.IsPopular)
                        .Select(PopularCategoryViewModel.FromSubcategoryEn);
            }

            this.ViewBag.StaticPagesFooter = footerViewModel;

            this.ViewBag.ManufacturersMainMenu =
                this.Data.Manufacturers.All()
                    .AsQueryable()
                    .Where(x => !x.IsDeleted && x.IsInTopMenu && x.Logo != string.Empty && x.Logo != null)
                    .Select(ManufacturerIndexViewModel.FromManufacturer);

            this.UserProfile = this.Data.Users.GetByUsername(requestContext.HttpContext.User.Identity.Name);

            var httpSessionStateBase = requestContext.HttpContext.Session;
            var cartManager = new CartManager(httpSessionStateBase);
            var shoppingCart = cartManager.GetShoppingCart();

            ShoppingCartMenuViewModel cart;

            if (shoppingCart == null)
            {
                cart = new ShoppingCartMenuViewModel { Total = 0, ProductCount = 0 };
            }
            else
            {
                cart = new ShoppingCartMenuViewModel
                           {
                               Total =
                                   this.Data.Products.All()
                                   .Where(LinqBuilder.BuildOrExpression<Product, int>(x => x.Id, shoppingCart.Items.Select(z => z.ProductId)))
                                   .Select(
                                       x => new { x.Id, x.Price })
                                   .ToList()
                                   .Sum(x => x.Price * shoppingCart.Items.Where(z => z.ProductId == x.Id).Sum(c => c.Quantity)),
                               ProductCount = shoppingCart.Items.Sum(x => x.Quantity)
                           };
            }

            this.ViewBag.Cart = cart;

            return base.BeginExecute(requestContext, callback, state);
        }

        private string GetSubDomain(RequestContext requestContext)
        {
            var url = requestContext.HttpContext.Request.Headers["HOST"];
            var index = url.IndexOf(".", StringComparison.Ordinal);

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