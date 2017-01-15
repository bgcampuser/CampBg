namespace CampBg.Web.Areas.Users.Controllers
{
    using System.Web.Mvc;

    using CampBg.Web.Controllers;

    [Authorize]
    public class ProfileController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Settings()
        {
            var userSettingsViewModel = new ProfileSettingsViewModel(this.UserProfile);

            return this.View(userSettingsViewModel);
        }

        [HttpPost]
        public ActionResult Settings(ProfileSettingsViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.TryUpdateModel(this.UserProfile);
                this.Data.SaveChanges();
                return this.RedirectToAction("Index", "Profile");
            }

            return this.View();
        }
    }
}