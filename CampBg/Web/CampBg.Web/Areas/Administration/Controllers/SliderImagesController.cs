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

    public class SliderImagesController : AdministrationBaseController
    {
        private const string SliderImagesFolder = "/SiteContent/SliderImages";

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetImages([DataSourceRequest]DataSourceRequest request)
        {
            var categories = this.Data.SliderImages.All().Select(SliderImageViewModel.FromSliderImage);

            return this.Json(categories.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult CreateImage([DataSourceRequest]DataSourceRequest request, SliderImageViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var sliderImage = new SliderImage();
                if (this.TryUpdateModel(sliderImage))
                {
                    this.Data.SliderImages.Add(sliderImage);
                    this.Data.SaveChanges();

                    model.Id = sliderImage.Id;

                    return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
                }
            }

            return null;
        }

        public ActionResult DeleteImage(SliderImageViewModel model)
        {
            var imageToDelete = this.Data.SliderImages.GetById(model.Id);

            if (imageToDelete != null)
            {
                this.Data.SliderImages.Delete(imageToDelete);
                this.Data.SaveChanges();
            }

            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateImage(SliderImageViewModel model)
        {
            var sliderImage = this.Data.SliderImages.GetById(model.Id);

            this.TryUpdateModel(sliderImage);
            this.Data.SaveChanges();

            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveImage(HttpPostedFileBase image, int id)
        {
            var sliderImage = this.Data.SliderImages.GetById(id);

            if (sliderImage != null)
            {
                var targetFolder = HostingEnvironment.MapPath(string.Format("~{0}/{1}", SliderImagesFolder, id));

                if (Directory.Exists(targetFolder))
                {
                    Directory.Delete(targetFolder, true);
                }

                Directory.CreateDirectory(targetFolder);

                this.ProcessImage(image, targetFolder);
                var imageRelativeLocation = string.Format("{0}/{1}/{2}", SliderImagesFolder, id, image.FileName);
                sliderImage.Location = imageRelativeLocation;
                this.Data.SaveChanges();
            }

            return this.Content(string.Empty);
        }

        private void ProcessImage(HttpPostedFileBase image, string targetFolder)
        {
            string imageLocation = string.Format("{0}/{1}", targetFolder, image.FileName);
            var webImage = new WebImage(image.InputStream);

            webImage.Resize(1151, 401, false);
            webImage.Crop(1, 1, 0, 0);
            webImage.Save(imageLocation);
        }
    }
}