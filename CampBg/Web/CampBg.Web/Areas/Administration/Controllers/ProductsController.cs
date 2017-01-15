﻿namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Hosting;
    using System.Web.Mvc;

    using CampBg.Data.Models;
    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class ProductsController : AdministrationBaseController
    {
        public ActionResult Index()
        {
            this.ViewBag.Properties = this.Data.Properties.All().Select(x => new
                                                                            {
                                                                                x.Id,
                                                                                x.Name
                                                                            });

            this.ViewBag.Manufacturers = this.Data.Manufacturers.All().Select(x => new
                                                                            {
                                                                                x.Id,
                                                                                x.Name
                                                                            });

            this.ViewBag.SubcategoryOptions =
                this.Data.SubcategoryOptions.All()
                    .Include(x => x.Subcategory)
                    .Include(x => x.Subcategory.Category)
                    .ToList()
                    .Select(
                        x =>
                        new
                            {
                                x.Id,
                                Name =
                            string.Format("{0} -> {1} -> {2}", x.Subcategory.Category.Name, x.Subcategory.Name, x.Name)
                            });

            return this.View();
        }

        public ActionResult GetProducts([DataSourceRequest]DataSourceRequest request)
        {
            var products = this.Data.Products.All().Select(ProductViewModel.FromProduct);

            return this.Json(products.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateProduct([DataSourceRequest]DataSourceRequest request, ProductViewModel model)
        {
                if (this.ModelState.IsValid)
                {
                    var product = new Product
                                      {
                                          Name = model.Name,
                                          NameEn = model.NameEn,
                                          Price = model.Price,
                                          Description = model.Description,
                                          DescriptionEn = model.DescriptionEn,
                                          ManufacturerId = model.ManufacturerId,
                                          SubcategoryOptionId = model.SubcategoryOptionId,
                                          IsPopular = model.IsPopular,
                                          ManufacturerIdentificationNumber = model.ManufacturerIdentificationNumber,
                                      };
                    this.Data.Products.Add(product);
                    this.Data.SaveChanges();
                    model.Id = product.Id;
                }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult UpdateProduct([DataSourceRequest] DataSourceRequest request, ProductViewModel model)
        {
            var product = this.Data.Products.GetById(model.Id);
            if (product == null)
            {
                this.ModelState.AddModelError("Id", "Invalid id was provided");
            }

            if (this.ModelState.IsValid)
            {

                product.Name = model.Name;
                product.NameEn = model.NameEn;
                product.Price = model.Price;
                product.Description = model.Description;
                product.DescriptionEn = model.DescriptionEn;
                product.ManufacturerId = model.ManufacturerId;
                product.SubcategoryOptionId = model.SubcategoryOptionId;
                product.IsPopular = model.IsPopular;
                product.ManufacturerIdentificationNumber = model.ManufacturerIdentificationNumber;

                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProduct([DataSourceRequest] DataSourceRequest request, ProductViewModel model)
        {
            var products = new List<ProductViewModel>();

            var product = this.Data.Products.GetById(model.Id);
            if (product == null)
            {
                this.ModelState.AddModelError("Id", "Invalid id");
            }

            this.Data.Products.Delete(product);
            this.Data.SaveChanges();

            return this.Json(products.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductImages([DataSourceRequest]DataSourceRequest request, int productId)
        {
            var productImages = this.Data.Products.GetById(productId)
                                                    .ProductImages
                                                    .AsQueryable()
                                                    .Select(ProductImageViewModel.FromProductImage);

            return this.Json(productImages.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveImages(IEnumerable<HttpPostedFileBase> images, int id)
        {
            var targetFolder = HostingEnvironment.MapPath(string.Format("~/SiteContent/Products/{0}/Images", id));

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            var product = this.Data.Products.GetById(id);
            foreach (var image in images)
            {
                var imageRelativeLocation = string.Format("/SiteContent/Products/{0}/Images/{1}", id, image.FileName);
                var thumbRelativeLocation = string.Format("/SiteContent/Products/{0}/Images/thumb_{1}", id, image.FileName);

                var imageLocation = HostingEnvironment.MapPath(imageRelativeLocation);
                var thumbLocation = HostingEnvironment.MapPath(thumbRelativeLocation);
                this.ProcessImage(image, imageLocation, thumbLocation);

                product.ProductImages.Add(new ProductImage
                {
                    Location = imageRelativeLocation,
                    ThumbnailLocation = thumbRelativeLocation
                });
            }

            var firstImage = product.ProductImages.FirstOrDefault();
            if (firstImage != null)
            {
                firstImage.IsDefault = true;
            }

            this.Data.SaveChanges();

            return this.Content(string.Empty);
        }

        public ActionResult ReadRelated([DataSourceRequest] DataSourceRequest request, int id)
        {
            var product = this.Data.Products.GetById(id);

            var related =
                product.RelatedProducts
                .AsQueryable()
                .Where(x => !x.IsDeleted)
                .Select(ProductRelationViewModel.FromProduct);

            return this.Json(related.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateRelated([DataSourceRequest] DataSourceRequest request, int id, ProductRelationViewModel model)
        {
            var product = this.Data.Products.GetById(id);
            var related = this.Data.Products.GetById(model.RelationId);

            if (related == null || product == null)
            {
                this.ModelState.AddModelError("RelationId", "Invalid relation id");
            }
            else
            {
                if (product.RelatedProducts.Any(x => x.Id == related.Id))
                {
                    this.ModelState.AddModelError("RelationId", "This relation already exists");
                }

                if (this.ModelState.IsValid)
                {
                    product.RelatedProducts.Add(related);
                    this.Data.SaveChanges();
                }
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadRelatedProducts()
        {
            var products =
                this.Data.Products.All()
                    .Select(ProductRelationViewModel.FromProduct);

            return this.Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveRelated([DataSourceRequest] DataSourceRequest request, int id, ProductRelationViewModel model)
        {
            var product = this.Data.Products.GetById(id);
            var relation = this.Data.Products.GetById(model.RelationId);

            if (id == model.RelationId)
            {
                this.ModelState.AddModelError("RelationId", "One item cannot be related to itself");
            }

            if (product != null && relation != null)
            {
                if (!product.RelatedProducts.Any(x => x.Id == model.RelationId))
                {
                    this.ModelState.AddModelError("RelationId", "This relation does not exist");
                }
                else
                {
                    if (this.ModelState.IsValid)
                    {
                        product.RelatedProducts.Remove(relation);
                        this.Data.SaveChanges();
                    }
                }
            }
            else
            {
                this.ModelState.AddModelError("RelationId", "Invalid relation");
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetDefaultImage(int id, int productId)
        {
            var allProducts = this.Data.Products.GetById(productId).ProductImages.ToList();
            allProducts.ForEach(x => x.IsDefault = false);
            allProducts.First(x => x.Id == id).IsDefault = true;
            this.Data.SaveChanges();

            return this.Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteImage(int id, int productId)
        {
            var product = this.Data.Products.GetById(productId);
            var imageToRemove = product.ProductImages.First(x => x.Id == id);
            product.ProductImages.Remove(imageToRemove);
            this.Data.SaveChanges();

            return this.Json("Success", JsonRequestBehavior.AllowGet);
        }

        private void ProcessImage(
            HttpPostedFileBase postedFile,
            string imageLocation,
            string thumbLocation)
        {
            var webImage = new WebImage(postedFile.InputStream);

            // temporarily save the original image
            webImage.Save(imageLocation + "_original");

            webImage.Resize(317, 342);
            webImage.Crop(1, 1, 1, 1);
            webImage.Save(imageLocation);

            webImage.Resize(172, 172);
            webImage.Crop(1, 1, 1, 1);
            webImage.Save(thumbLocation);
        }
    }
}