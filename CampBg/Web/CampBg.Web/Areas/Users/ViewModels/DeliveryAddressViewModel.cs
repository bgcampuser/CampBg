namespace CampBg.Web.Areas.Users.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;
    using CampBg.Web.Localization;

    public class DeliveryAddressViewModel
    {
        public static Expression<Func<DeliveryAddress, DeliveryAddressViewModel>> FromDeliveryAddress
        {
            get
            {
                return addr => new DeliveryAddressViewModel
                                   {
                                       Id = addr.Id,
                                       Address = addr.Address,
                                       City = addr.City,
                                       PostalCode = addr.PostalCode
                                   };
            }
        }

        public int? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(ResourceType = typeof(ViewModels), Name = "City")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(ResourceType = typeof(ViewModels), Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(ViewModels), Name = "Postal_Code")]
        public int PostalCode { get; set; }
    }
}