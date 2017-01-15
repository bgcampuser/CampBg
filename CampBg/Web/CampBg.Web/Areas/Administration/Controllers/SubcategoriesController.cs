namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CampBg.Data.Models;
    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class SubcategoriesController : AdministrationBaseController
    {
        public ActionResult Index()
        {
            this.ViewBag.Categories = this.Data.Categories.All().Select(CategoryViewModel.FromCategory).ToList();
            this.ViewBag.Categories.Insert(0, new CategoryViewModel { Id = 0, Name = "Please select" });
            return this.View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var propertyCategories =
                this.Data.Subcategories.All().Select(SubcategoryViewModel.FromSubcategory);

            return this.Json(propertyCategories.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create([DataSourceRequest]DataSourceRequest request, SubcategoryViewModel model)
        {
            var categories = new List<SubcategoryViewModel> { model };

            if (this.ModelState.IsValid)
            {
                var propertyCategory = new Subcategory();
                this.TryUpdateModel(propertyCategory);

                this.Data.Subcategories.Add(propertyCategory);
                this.Data.SaveChanges();
                model.Id = propertyCategory.Id;
            }

            return this.Json(categories.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update([DataSourceRequest]DataSourceRequest request, SubcategoryViewModel model)
        {
            var categories = new List<SubcategoryViewModel> { model };

            var subcategory = this.Data.Subcategories.GetById(model.Id);

            if (model.CategoryId == 0)
            {
                this.ModelState.AddModelError("CategoryId", "Please select category");
            }

            if (this.ModelState.IsValid)
            {
                this.TryUpdateModel(subcategory);
                this.Data.SaveChanges();
            }

            return this.Json(categories.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, SubcategoryViewModel model)
        {
            var categories = new List<SubcategoryViewModel> { model };

            var subcategory = this.Data.Subcategories.GetById(model.Id);
            if (subcategory == null)
            {
                this.ModelState.AddModelError("Id", "Invalid id");
            }

            if (this.ModelState.IsValid)
            {
                this.Data.Subcategories.Delete(subcategory);
                this.Data.SaveChanges();
            }

            return this.Json(categories.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }
    }
}