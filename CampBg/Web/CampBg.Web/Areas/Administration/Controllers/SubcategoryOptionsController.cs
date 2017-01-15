namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    using CampBg.Data.Models;
    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class SubcategoryOptionsController : AdministrationBaseController
    {
        public ActionResult Index()
        {
            this.ViewBag.Subcategories =
                this.Data.Subcategories.All()
                    .Include(x => x.Category)
                    .ToList()
                    .Select(
                        x =>
                        new SubcategoryViewModel
                            {
                                Id = x.Id,
                                Name = string.Format("{0} ({1})", x.Name, x.Category.Name),
                            });

            return this.View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var subcategoryOptions = this.Data.SubcategoryOptions.All().Select(SubcategoryOptionViewModel.FromSubcategoryOption);

            return this.Json(subcategoryOptions.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create([DataSourceRequest]DataSourceRequest request, SubcategoryOptionViewModel model)
        {
            var subcategoryOptions = new List<SubcategoryOptionViewModel> { model };

            if (this.ModelState.IsValid)
            {
                var subcategoryOption = new SubcategoryOption();
                this.TryUpdateModel(subcategoryOption);

                this.Data.SubcategoryOptions.Add(subcategoryOption);
                this.Data.SaveChanges();
                model.Id = subcategoryOption.Id;
            }

            return this.Json(subcategoryOptions.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update([DataSourceRequest]DataSourceRequest request, SubcategoryOptionViewModel model)
        {
            var subcategoryOptions = new List<SubcategoryOptionViewModel> { model };

            var subcategoryOption = this.Data.SubcategoryOptions.GetById(model.Id);
            if (subcategoryOption == null)
            {
                this.ModelState.AddModelError("Id", "Invalid id");
            }

            if (this.ModelState.IsValid)
            {
                this.TryUpdateModel(subcategoryOption);
                this.Data.SaveChanges();
            }

            return this.Json(subcategoryOptions.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, SubcategoryOptionViewModel model)
        {
            var subcategoryOptions = new List<SubcategoryOptionViewModel> { model };

            var subcategoryOption = this.Data.SubcategoryOptions.GetById(model.Id);
            if (subcategoryOption == null)
            {
                this.ModelState.AddModelError("Id", "Invalid id");
            }

            if (this.ModelState.IsValid)
            {
                this.Data.SubcategoryOptions.Delete(subcategoryOption);
                this.Data.SaveChanges();
            }

            return this.Json(subcategoryOptions.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadSubcategories([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Subcategories.All().Include(x => x.Category)
                .ToList().Select(x => new SubcategoryViewModel { Id = x.Id, Name = string.Format("{0} ({1})", x.Name, x.Category.Name) });

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}