namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using CampBg.Data.Models;
    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class CategoriesController : AdministrationBaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetCategories([DataSourceRequest]DataSourceRequest request)
        {
            var categories = this.Data.Categories.All().Select(CategoryViewModel.FromCategory);

            return this.Json(categories.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult CreateCategory([DataSourceRequest]DataSourceRequest request, CategoryViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var category = new Category();
                if (this.TryUpdateModel(category))
                {
                    this.Data.Categories.Add(category);
                    this.Data.SaveChanges();

                    model.Id = category.Id;

                    return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
                }
            }

            return null;
        }

        public ActionResult DeleteCategory(CategoryViewModel model)
        {
            var categoryToDelete = this.Data.Categories
                .GetById(model.Id);

            if (categoryToDelete != null)
            {
                this.Data.Categories.Delete(categoryToDelete);
                this.Data.SaveChanges();
            }

            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateCategory(CategoryViewModel category)
        {
            var categoryToUpdate = this.Data.Categories.GetById(category.Id);

            this.TryUpdateModel(categoryToUpdate);
            this.Data.SaveChanges();

            return this.Json(category, JsonRequestBehavior.AllowGet);
        }
    }
}