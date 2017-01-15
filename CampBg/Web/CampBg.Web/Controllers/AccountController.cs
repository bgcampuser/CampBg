namespace CampBg.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using CampBg.Data;
    using CampBg.Data.Models;
    using CampBg.Web.Models;
    using CampBg.Web.ViewModels;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;

    using OJS.Common;

    using ResourceEmails = CampBg.Web.Localization.EmailTemplates;
    using ResourceViews = CampBg.Web.Localization.Views;

    [Authorize]
    public class AccountController : BaseController
    {
        private const string XsrfKey = "XsrfId";

        public AccountController()
            : this(new UserManager<UserProfile>(new UserStore<UserProfile>(new CampContext())))
        {
        }

        public AccountController(UserManager<UserProfile> userManager)
        {
            this.UserManager = userManager;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        public UserManager<UserProfile> UserManager { get; private set; }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.UserManager.FindAsync(model.UserName, model.Password);
                var userProfile = this.Data.Users.GetByUsername(model.UserName);
                if (user != null && userProfile != null && !userProfile.IsDeleted)
                {
                    await this.SignInAsync(user, model.RememberMe);
                    return this.RedirectToLocal(returnUrl);
                }

                this.ModelState.AddModelError(string.Empty, ResourceViews.Invalid_username_or_password);
            }

            this.ViewBag.ReturnUrl = returnUrl;
            return this.View(model);
        }

        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string returnUrl)
        {
#if !DEBUG
            if (this.Data.Users.All().Any(x => x.Email == model.Email))
            {
                this.ModelState.AddModelError("Email", ResourceViews.Email_already_registered);
            }
#endif

            if (this.ModelState.IsValid)
            {
                var user = new UserProfile
                               {
                                   UserName = model.UserName,
                                   Email = model.Email,
                                   IsSubscribedForNewsletter = model.IsSubscribedForNewsletter,
                                   PhoneNumber = model.PhoneNumber,
                                   CreatedOn = DateTime.Now
                               };

                var result = await this.UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    try
                    {
                        this.MailSender.SendMail(model.Email, ResourceEmails.Welcome_email_title, ResourceEmails.Welcome_email_body);
                    }
                    catch (Exception)
                    {
                        // TODO: add error handling
                    }

                    await this.SignInAsync(user, isPersistent: false);
                    return this.RedirectToLocal(returnUrl);
                }

                this.AddErrors(result);
            }

            this.ViewBag.ReturnUrl = returnUrl;
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            var result = await this.UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            ManageMessageId? message = result.Succeeded ? ManageMessageId.RemoveLoginSuccess : ManageMessageId.Error;

            return this.RedirectToAction("Manage", new { Message = message });
        }

        public ActionResult Manage(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? ResourceViews.Password_changed_successfully
                : message == ManageMessageId.SetPasswordSuccess ? ResourceViews.Pasword_set_successfully
                : message == ManageMessageId.RemoveLoginSuccess ? ResourceViews.External_login_removed
                : message == ManageMessageId.Error ? ResourceViews.Error
                : string.Empty;
            this.ViewBag.HasLocalPassword = this.HasPassword();
            this.ViewBag.ReturnUrl = Url.Action("Manage");
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = this.HasPassword();
            this.ViewBag.HasLocalPassword = hasPassword;
            this.ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (this.ModelState.IsValid)
                {
                    var result = await this.UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }

                    this.AddErrors(result);
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                var state = this.ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (this.ModelState.IsValid)
                {
                    IdentityResult result = await this.UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }

                    this.AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return this.RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await this.UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await this.SignInAsync(user, isPersistent: false);
                return this.RedirectToLocal(returnUrl);
            }

            // If the user does not have an account, then prompt the user to create an account
            this.ViewBag.ReturnUrl = returnUrl;
            this.ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
            return this.View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
        }

        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return this.RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }

            var result = await this.UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return this.RedirectToAction("Manage");
            }

            return this.RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Manage");
            }

            if (this.ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await this.AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return this.View("ExternalLoginFailure");
                }

                var user = new UserProfile { UserName = model.UserName };
                var result = await this.UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await this.UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await this.SignInAsync(user, isPersistent: false);
                        return this.RedirectToLocal(returnUrl);
                    }
                }

                this.AddErrors(result);
            }

            this.ViewBag.ReturnUrl = returnUrl;
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(string returnUrl)
        {
            this.AuthenticationManager.SignOut();
            return this.RedirectToLocal(returnUrl);
        }

        public ActionResult RegistrationSuccessfull()
        {
            return this.View();
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return this.View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = this.UserManager.GetLogins(User.Identity.GetUserId());
            this.ViewBag.ShowRemoveButton = this.HasPassword() || linkedAccounts.Count > 1;
            return this.PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        #region Forgotten password
        [AllowAnonymous]
        public ActionResult ForgottenPassword()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgottenPassword(string email)
        {
            var user = this.Data.Users.All().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                // TODO: error
                return this.RedirectToAction("Login");
            }

            user.ForgottenPasswordToken = Guid.NewGuid();
            this.Data.SaveChanges();
            this.SendForgottenPasswordToUser(user);

            return this.RedirectToAction("ForgottenPasswordSent");
        }

        [AllowAnonymous]
        public ActionResult ForgottenPasswordSent()
        {
            return this.View();
        }

        [AllowAnonymous]
        public ActionResult ChangePassword(string token)
        {
            Guid guid;

            if (!Guid.TryParse(token, out guid))
            {
                // TODO: error
                return this.RedirectToAction("Login");
            }

            var user = this.Data.Users.All().FirstOrDefault(x => x.ForgottenPasswordToken == guid);

            if (user == null)
            {
                // TODO: error;
                return this.RedirectToAction("Login");
            }

            var forgottenPasswordModel = new ForgottenPasswordViewModel
            {
                Token = guid
            };

            return this.View(forgottenPasswordModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ChangePassword(ForgottenPasswordViewModel model)
        {
            var user = this.Data.Users.All()
                .FirstOrDefault(x => x.ForgottenPasswordToken == model.Token);

            if (user == null)
            {
                // TODO: error
                return this.RedirectToAction("Login");
            }

            if (this.ModelState.IsValid)
            {
                IdentityResult removePassword =
                                        await
                                        this.UserManager.RemovePasswordAsync(user.Id);
                if (removePassword.Succeeded)
                {
                    IdentityResult changePassword =
                                        await
                                        this.UserManager.AddPasswordAsync(user.Id, model.Password);

                    if (changePassword.Succeeded)
                    {
                        user.ForgottenPasswordToken = null;
                        this.Data.SaveChanges();

                        return this.RedirectToAction("Login");
                    }

                    this.AddErrors(changePassword);
                }

                this.AddErrors(removePassword);
            }

            return this.View(model);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.UserManager != null)
            {
                this.UserManager.Dispose();
                this.UserManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return this.HttpContext.GetOwinContext().Authentication;
            }
        }

        private void SendForgottenPasswordToUser(UserProfile user)
        {
            var emailBody = this.buildEmailBody(user);

            this.MailSender.SendMail(user.Email, ResourceEmails.Forgotten_password_email_title, emailBody);
        }

        private string buildEmailBody(UserProfile user)
        {
            var forgottenPasswordBody = string.Format(ResourceEmails.Forgotten_password_email_body, user.UserName, Url.Action(
                                                        "ChangePassword",
                                                        "Account",
                                                        new { token = user.ForgottenPasswordToken },
                                                        protocol: Request.Url.Scheme));
            return forgottenPasswordBody;
        }

        private async Task SignInAsync(UserProfile user, bool isPersistent)
        {
            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await this.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            this.AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }

        private bool HasPassword()
        {
            var user = this.UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                this.LoginProvider = provider;
                this.RedirectUri = redirectUri;
                this.UserId = userId;
            }

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = this.RedirectUri };
                if (this.UserId != null)
                {
                    properties.Dictionary[XsrfKey] = this.UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, this.LoginProvider);
            }
        }
        #endregion
    }
}