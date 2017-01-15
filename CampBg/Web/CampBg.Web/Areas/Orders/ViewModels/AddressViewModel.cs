namespace CampBg.Web.Areas.Orders.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class AddressViewModel
    {
        public static Expression<Func<DeliveryAddress, AddressViewModel>> FromDeliveryAddress
        {
            get
            {
                return
                    addr =>
                    new AddressViewModel
                        {
                            Id = addr.Id,
                            Address = addr.Address,
                            City = addr.City,
                            PostalCode = addr.PostalCode
                        };
            }
        }

        public int Id { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public int PostalCode { get; set; }
    }
}