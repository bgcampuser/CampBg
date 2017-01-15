namespace CampBg.Web.Areas.Orders.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using ViewModels = CampBg.Web.Localization.ViewModels;

    public class OrderViewModel
    {
        public int? AddressId { get; set; }

        [UIHint("MultiLineText")]
        [Display(ResourceType = typeof(ViewModels), Name = "Address")]
        public string Address { get; set; }
    }
}