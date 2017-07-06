namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using CampBg.Data;
    using CampBg.Data.Models;
    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UsersController : AdministrationBaseController
    {
        public UsersController()
            : this(new UserManager<UserProfile>(new UserStore<UserProfile>(new CampContext())))
        {
        }

        public UsersController(UserManager<UserProfile> userManager)
        {
            this.UserManager = userManager;
        }

        public UserManager<UserProfile> UserManager { get; private set; }

        public ActionResult Index()
        {
            this.ViewBag.IdentityRoles = this.Data.IdentityRoles.All().ToList();

            this.ViewBag.UserRoles = this.Data.IdentityRoles.All().Select(r => new UserRoleViewModel { Text = r.Name, Id = r.Id }).ToList();

            return this.View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var users = this.Data.Users.All().Select(UserViewModel.FromUserProfile);

            return this.Json(users.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult Destroy(UserViewModel model)
        {
            var userToDelete = this.Data.Users
                .GetById(model.Id);

            if (userToDelete != null)
            {
                this.Data.Users.Delete(userToDelete);
                this.Data.SaveChanges();
            }

            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(UserViewModel model)
        {
            var userToUpdate = this.Data.Users.GetById(model.Id);

            userToUpdate.UserName = model.UserName;
            userToUpdate.Email = model.Email;
            userToUpdate.IsSubscribedForNewsletter = model.IsSubscribedForNewsletter;
            userToUpdate.IsDeleted = model.IsDeleted;

            var roles = this.Data.IdentityRoles.All();

            foreach (var role in roles)
            {
                this.UserManager.RemoveFromRole(userToUpdate.Id, role.Name);
            }

            foreach (var role in model.UserRoles)
            {
                string roleText = roles.First(x => x.Id == role.Id).Name;
                this.UserManager.AddToRole(userToUpdate.Id, roleText);
            }


            this.Data.SaveChanges();

            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadAddresses([DataSourceRequest] DataSourceRequest request, string userId)
        {
            var userAddress =
                this.Data.Users.GetById(userId).DeliveryAddresses.AsQueryable().Select(UserAddressViewModel.FromAddress);

            return this.Json(userAddress.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DestroyAddresses(string userId, UserAddressViewModel model)
        {
            var user = this.Data.Users.GetById(userId);
            var userAddressToRemove =
                user.DeliveryAddresses.FirstOrDefault(x => x.Id == model.Id);

            if (userAddressToRemove != null)
            {
                user.DeliveryAddresses.Remove(userAddressToRemove);
                this.Data.SaveChanges();
            }

            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAddresses([DataSourceRequest] DataSourceRequest request, string userId, UserAddressViewModel model)
        {
            var user = this.Data.Users.GetById(userId);
            var addressToUpdate = user.DeliveryAddresses.FirstOrDefault(x => x.Id == model.Id);
            if (addressToUpdate != null && this.ModelState.IsValid)
            {
                this.TryUpdateModel(addressToUpdate);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateAddresses([DataSourceRequest] DataSourceRequest request, string userId, UserAddressViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = this.Data.Users.GetById(userId);
                var address = new DeliveryAddress();
                this.TryUpdateModel(address);
                user.DeliveryAddresses.Add(address);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }
    }
}