namespace CampBg.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using CampBg.Web.Localization;

    public class LoginViewModel
    {
        [Required]
        [Display(ResourceType = typeof(ViewModels), Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewModels), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "Remember_me_")]
        public bool RememberMe { get; set; }
    }
}
