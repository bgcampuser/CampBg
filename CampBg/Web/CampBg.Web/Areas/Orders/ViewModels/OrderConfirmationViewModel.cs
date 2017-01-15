namespace CampBg.Web.Areas.Orders.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CampBg.Web.Localization;

    public class OrderConfirmationViewModel : OrderViewModel
    {
        private IEnumerable<AddressViewModel> addresses;

        public CartViewModel Cart { get; set; }

        public IEnumerable<AddressViewModel> Addresses
        {
            get
            {
                return this.addresses ?? (this.addresses = new HashSet<AddressViewModel>());
            }

            set
            {
                this.addresses = value;
            }
        }

        [UIHint("String")]
        [Display(ResourceType = typeof(ViewModels), Name = "Phone_number")]
        public string PhoneNumber { get; set; }

        [UIHint("String")]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(ViewModels), Name = "Email_address")]
        public string EmailAddress { get; set; }

        [UIHint("MultiLineText")]
        [Display(ResourceType = typeof(ViewModels), Name = "Details")]
        public string AdditionalDetails { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "Postal_Code")]
        [Required]
        public int PostalCode { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "City")]
        [Required]
        public string City { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "Payment_method")]
        [Required]
        public PaymentMethodViewModel PaymentMethod { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "Company_name")]
        public string CompanyName { get; set; }

        [UIHint("MultiLineText")]
        [Display(ResourceType = typeof(ViewModels), Name = "Company_address")]
        public string CompanyAddress { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "EIK")]
        public string EIK { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "VATNumber")]
        public string VATNumber { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "Custodian")]
        public string Custodian { get; set; }

        public bool Receipt { get; set; }
    }
}