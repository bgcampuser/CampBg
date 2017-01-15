namespace CampBg.Web.Areas.Orders.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using CampBg.Web.Localization;

    public enum PaymentMethodViewModel
    {
        [Display(ResourceType = typeof(ViewModels), Name = "Upon_delivery")]
        UponDelivery = 0,
        [Display(ResourceType = typeof(ViewModels), Name = "Bank_transfer")]
        BankTransfer = 1
    }
}