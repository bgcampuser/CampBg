namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CampBg.Data.Models;
    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class PropertiesController : AdministrationBaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var properties = this.Data.Properties
                                            .All()
                                            .Select(PropertyViewModel.FromProperty);

            return this.Json(properties.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create([DataSourceRequest]DataSourceRequest request, PropertyViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var property = new Property();
                if (this.TryUpdateModel(property))
                {
                    this.Data.Properties.Add(property);
                    this.Data.SaveChanges();

                    model.Id = property.Id;

                    return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
                }
            }

            return null;
        }

        public ActionResult Update([DataSourceRequest]DataSourceRequest request, PropertyViewModel model)
        {
            var properties = new List<PropertyViewModel> { model };

            var property = this.Data.Properties.GetById(model.Id);

            if (property == null)
            {
                this.ModelState.AddModelError("Id", "Invalid model id");
            }

            if (this.ModelState.IsValid)
            {
                this.TryUpdateModel(property);
                this.Data.SaveChanges();
            }

            return this.Json(properties.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, PropertyViewModel model)
        {
            var properties = new List<PropertyViewModel> { model };

            var property = this.Data.Properties.GetById(model.Id);

            if (property == null)
            {
                this.ModelState.AddModelError("Id", "Invalid model id");
            }

            if (this.ModelState.IsValid)
            {
                this.Data.Properties.Delete(property);
                this.Data.SaveChanges();
            }

            return this.Json(properties.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadProductProperties([DataSourceRequest] DataSourceRequest request, int productId)
        {
            var productProperties =
                this.Data.Products.GetById(productId)
                    .PropertyValues.Where(x => !x.IsDeleted).AsQueryable()
                    .Select(ProductPropertyViewModel.FromPropertyValue);

            return this.Json(productProperties.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateProductProperties([DataSourceRequest]DataSourceRequest request, ProductPropertyViewModel model, int productId)
        {
            var properties = new List<ProductPropertyViewModel> { model };

            if (this.ModelState.IsValid)
            {
                var propertyValue =
                    this.Data.PropertyValues.All()
                        .FirstOrDefault(x => x.PropertyId == model.PropertyId && x.Value == model.Value && x.ValueEn == model.ValueEn);

                var product = this.Data.Products.GetById(productId);

                if (propertyValue == null)
                {
                    propertyValue = new PropertyValue { PropertyId = model.PropertyId, Value = model.Value, ValueEn = model.ValueEn };
                    propertyValue.Products.Add(product);
                    this.Data.PropertyValues.Add(propertyValue);
                }
                else
                {
                    propertyValue.Products.Add(product);
                }

                this.Data.SaveChanges();

                model.Id = propertyValue.Id;
            }

            return this.Json(properties.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateProductProperties([DataSourceRequest]DataSourceRequest request, ProductPropertyViewModel model)
        {
            var properties = new List<ProductPropertyViewModel> { model };

            var property = this.Data.PropertyValues.GetById(model.Id);

            if (property == null)
            {
                this.ModelState.AddModelError("Id", "Invalid model id");
            }

            if (this.ModelState.IsValid)
            {
                this.TryUpdateModel(property);
                this.Data.SaveChanges();
            }

            return this.Json(properties.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProductProperties([DataSourceRequest]DataSourceRequest request, ProductPropertyViewModel model)
        {
            var properties = new List<ProductPropertyViewModel> { model };

            var property = this.Data.PropertyValues.GetById(model.Id);

            if (property == null)
            {
                this.ModelState.AddModelError("Id", "Invalid model id");
            }

            if (this.ModelState.IsValid)
            {
                this.Data.PropertyValues.Delete(property);
                this.Data.SaveChanges();
            }

            return this.Json(properties.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }
    }
}