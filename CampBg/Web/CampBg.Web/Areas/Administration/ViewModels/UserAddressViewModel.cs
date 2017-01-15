namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class UserAddressViewModel
    {
        public static Expression<Func<DeliveryAddress, UserAddressViewModel>> FromAddress
        {
            get
            {
                return
                    addr =>
                    new UserAddressViewModel
                        {
                            Id = addr.Id,
                            Address = addr.Address,
                            City = addr.City,
                            PostalCode = addr.PostalCode
                        };
            }
        }

        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        public string City { get; set; }
        
        public int PostalCode { get; set; }
    }
}