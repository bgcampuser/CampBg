namespace CampBg.Web.Areas.Administration.Controllers
{
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

    public class ManufacturersController : AdministrationBaseController
    {
        private const string ManufacturerImageLocation = "/SiteContent/Manufacturers";

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var categories = this.Data.Manufacturers.All().Select(ManufacturerViewModel.FromManufacturer);

            return this.Json(categories.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ManufacturerViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var manufacturer = new Manufacturer();

                if (this.TryUpdateModel(manufacturer))
                {
                    this.Data.Manufacturers.Add(manufacturer);
                    this.Data.SaveChanges();
                    model.Id = manufacturer.Id;

                    return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
                }
            }

            return null;
        }

        public ActionResult Destroy(ManufacturerViewModel model)
        {
            var manufacturerToDelete = this.Data.Manufacturers
                .GetById(model.Id);

            if (manufacturerToDelete != null)
            {
                this.Data.Manufacturers.Delete(manufacturerToDelete);
                this.Data.SaveChanges();
            }

            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(ManufacturerViewModel model)
        {
            var manufacturerToUpdate = this.Data.Manufacturers.GetById(model.Id);

            this.TryUpdateModel(manufacturerToUpdate);
            this.Data.SaveChanges();

            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveImage(HttpPostedFileBase image, int id)
        {
            var manufacturer = this.Data.Manufacturers.GetById(id);

            if (manufacturer != null)
            {
                var targetFolder = HostingEnvironment.MapPath(string.Format("~{0}/{1}", ManufacturerImageLocation, id));

                if (Directory.Exists(targetFolder))
                {
                    Directory.Delete(targetFolder, true);
                }

                Directory.CreateDirectory(targetFolder);

                this.ProcessImage(image, targetFolder);
                var imageRelativeLocation = string.Format("{0}/{1}/{2}", ManufacturerImageLocation, id, image.FileName);
                manufacturer.Logo = imageRelativeLocation;
                this.Data.SaveChanges();
            }

            return this.Content(string.Empty);
        }

        private void ProcessImage(HttpPostedFileBase image, string targetFolder)
        {
            string imageLocation = string.Format("{0}/{1}", targetFolder, image.FileName);
            var webImage = new WebImage(image.InputStream);

            webImage.Resize(161, 61, false);
            webImage.Crop(1, 1, 0, 0);
            webImage.Save(imageLocation);
        }
    }
}