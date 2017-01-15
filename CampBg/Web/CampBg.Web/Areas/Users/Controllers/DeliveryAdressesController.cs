namespace CampBg.Web.Areas.Users.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using CampBg.Data.Models;
    using CampBg.Web.Areas.Users.ViewModels;
    using CampBg.Web.Controllers;

    [Authorize]
    public class DeliveryAdressesController : BaseController
    {
        public ActionResult Index()
        {
            var registeredAddresses =
                this.UserProfile.DeliveryAddresses.AsQueryable().Where(x => !x.IsDeleted)
                    .Select(DeliveryAddressViewModel.FromDeliveryAddress);

            return this.View(registeredAddresses);
        }

        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(DeliveryAddressViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var address = new DeliveryAddress();
                this.TryUpdateModel(address);
                this.UserProfile.DeliveryAddresses.Add(address);
                this.Data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        public ActionResult Edit(int id)
        {
            var addr =
                this.UserProfile.DeliveryAddresses.AsQueryable()
                    .Where(x => x.Id == id)
                    .Select(DeliveryAddressViewModel.FromDeliveryAddress).FirstOrDefault();

            if (addr != null)
            {
                return this.View(addr);
            }

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DeliveryAddressViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var address =
                    this.UserProfile.DeliveryAddresses.AsQueryable().FirstOrDefault(x => x.Id == model.Id);

                if (address != null)
                {
                    this.TryUpdateModel(address);
                    this.Data.SaveChanges();
                }

                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        public ActionResult Remove(int id)
        {
            var userAddress = this.UserProfile.DeliveryAddresses.FirstOrDefault(x => x.Id == id);
            if (userAddress != null)
            {
                userAddress.IsDeleted = true;
                userAddress.DeletedOn = DateTime.Now;
                this.Data.SaveChanges();
            }

            return this.RedirectToAction("Index");
        }
    }
}