namespace CampBg.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    using CampBg.Web.ViewModels;

    using StaticPageViewModel = CampBg.Web.ViewModels.StaticPageViewModel;

    public class HomeController : BaseController
    {
        private const int PageSize = 8;

        private const int MaxPageSize = 20;

        public ActionResult Index()
        {
            var indexViewModel = new IndexPageViewModel
                                     {
                                         SliderImages =
                                             this.Data.SliderImages.All()
                                             .Where(img => img.IsActive)
                                             .Select(SliderImageViewModel.FromSliderImage)
                                     };

            return this.View(indexViewModel);
        }

        public ActionResult Language(string lang, string returnUrl)
        {
            if (this.Locales.ContainsKey(lang)) 
            {
                var cookie = new HttpCookie("culture") { Value = lang };
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }

            return this.RedirectToLocal(returnUrl);
        }
             
        public ActionResult Page(string page)
        {
            StaticPageViewModel staticPage;

            if (Thread.CurrentThread.CurrentCulture.Name == "bg-BG")
            {
                staticPage =
                    this.Data.StaticPages.All()
                        .Where(x => x.AddressBarName == page)
                        .Select(StaticPageViewModel.FromStaticPageBg)
                        .FirstOrDefault();
            }
            else
            {
                staticPage =
                    this.Data.StaticPages.All()
                        .Where(x => x.AddressBarName == page)
                        .Select(StaticPageViewModel.FromStaticPageEn)
                        .FirstOrDefault();
            }

            return this.View(staticPage);
        }

        public ActionResult GetLatestProducts(int page = 0)
        {
            var productListMultipleViewModel = new ProductListMultipleViewModel();

            IEnumerable<ProductListViewModel> latestProducts;

            if (Thread.CurrentThread.CurrentCulture.Name == "bg-BG")
            {
                latestProducts =
                    this.Data.Products.All()
                        .OrderByDescending(product => product.CreatedOn)
                        .Skip(page * PageSize)
                        .Take(PageSize)
                        .Select(ProductListViewModel.FromProduct);
            }
            else
            {
                latestProducts =
                    this.Data.Products.All()
                        .OrderByDescending(product => product.CreatedOn)
                        .Skip(page * PageSize)
                        .Take(PageSize)
                        .Select(ProductListViewModel.FromProductEn);
            }

            var totalPages = (int)Math.Ceiling((decimal)this.Data.Products.All().Count() / PageSize);

            productListMultipleViewModel.Products = latestProducts;
            productListMultipleViewModel.TotalPages = totalPages < 20 ? totalPages : MaxPageSize;

            return this.PartialView("HomePageProductsPartial", productListMultipleViewModel);
        }

        public ActionResult GetPopularProducts(int page = 0)
        {
            var productListMultipleViewModel = new ProductListMultipleViewModel();

            IEnumerable<ProductListViewModel> popularProducts;

            if (Thread.CurrentThread.CurrentCulture.Name == "bg-BG")
            {
                popularProducts =
                    this.Data.Products.All()
                        .AsQueryable()
                        .Where(x => x.IsPopular)
                        .OrderBy(x => Guid.NewGuid())
                        .Skip(page * PageSize)
                        .Take(PageSize)
                        .Select(ProductListViewModel.FromProduct);
            }
            else
            {
                popularProducts =
                    this.Data.Products.All()
                        .AsQueryable()
                        .Where(x => x.IsPopular)
                        .OrderBy(x => Guid.NewGuid())
                        .Skip(page * PageSize)
                        .Take(PageSize)
                        .Select(ProductListViewModel.FromProductEn);
            }

            productListMultipleViewModel.Products = popularProducts;
            productListMultipleViewModel.TotalPages =
                (int)Math.Ceiling((decimal)this.Data.Products.All().Count(x => x.IsPopular) / PageSize);

            return this.PartialView("HomePageProductsPartial", productListMultipleViewModel);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }
    }
}