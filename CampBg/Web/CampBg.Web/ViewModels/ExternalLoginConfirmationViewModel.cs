namespace CampBg.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using CampBg.Web.Localization;

    /// <summary>
    /// The external login confirmation view model.
    /// </summary>
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(ResourceType = typeof(ViewModels), Name = "Username")]
        public string UserName { get; set; }
    }
}