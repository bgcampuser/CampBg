namespace CampBg.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CampBg.Web.Localization;

    public class ForgottenPasswordViewModel
    {
        [Display(ResourceType = typeof(ViewModels), Name = "Password")]
        [StringLength(100, ErrorMessageResourceType = typeof(ViewModels), ErrorMessageResourceName = "Password_error_message", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(ViewModels), ErrorMessageResourceName = "Password_compare_error")]
        [Display(ResourceType = typeof(ViewModels), Name = "Confirm_password")]
        public string PasswordConfirm { get; set; }

        public Guid Token { get; set; }
    }
}