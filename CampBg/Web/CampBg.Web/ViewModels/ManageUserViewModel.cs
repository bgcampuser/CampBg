namespace CampBg.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using CampBg.Web.Localization;

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewModels), Name = "Current_password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(ViewModels), ErrorMessageResourceName = "Password_error_message", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewModels), Name = "New_password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewModels), Name = "Confirm_new_password")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(ViewModels), ErrorMessageResourceName = "Password_compare_error")]
        public string ConfirmPassword { get; set; }
    }
}