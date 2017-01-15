namespace CampBg.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CampBg.Web.Localization;

    public class RegisterViewModel : IValidatableObject
    {
        [Required(ErrorMessageResourceType = typeof(ViewModels), ErrorMessageResourceName = "Username_required")]
        [Display(ResourceType = typeof(ViewModels), Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewModels), ErrorMessageResourceName = "Password_required")]
        [StringLength(100, ErrorMessageResourceType = typeof(ViewModels), ErrorMessageResourceName = "Password_error_message", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewModels), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewModels), Name = "Confirm_password")]
        [Compare("Password", ErrorMessageResourceType = typeof(ViewModels), ErrorMessageResourceName = "Password_compare_error")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required(ErrorMessageResourceType = typeof(ViewModels), ErrorMessageResourceName = "Email_required")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Display(ResourceType = typeof(ViewModels), Name = "Phone_number")]
        [Required(ErrorMessageResourceType = typeof(ViewModels), ErrorMessageResourceName = "Phone_required")]
        public string PhoneNumber { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "Subscribe_for_newsletter")]
        [DefaultValue(true)]
        public bool IsSubscribedForNewsletter { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (this.UserName.IndexOf(' ') > -1)
            {
                results.Add(new ValidationResult(ViewModels.Username_no_spaces, new[] { "DisplayName" }));
            }

            return results;
        }
    }
}