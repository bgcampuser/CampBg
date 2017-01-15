namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class OrderViewModel
    {
        public static Expression<Func<Order, OrderViewModel>> FromOrder
        {
            get
            {
                return
                    order => new OrderViewModel
                    {
                        Id = order.Id,
                        User = order.User,
                        UserId = order.UserId,
                        ByUser = order.OrderedBy,
                        MadeOn = order.CreatedOn,
                        IsPaid = order.IsPaid,
                        IsFinalized = order.IsFinalized,
                        PhoneNumber = order.PhoneNumber,
                        Total = order.OrderItems.Sum(x => x.Quantity * x.Price),
                        AdditionalDetails = order.AdditionalDetails,
                        PaymentMethod = order.PaymentMethod,
                        Address = order.DeliveryAddress.Address,
                        City = order.DeliveryAddress.City,
                        PostalCode = order.DeliveryAddress.PostalCode,
                        OrderDate = order.CreatedOn,
                        EmailAddress = order.EmailAddress,
                        CompanyAddress = order.CompanyAddress,
                        CompanyName = order.CompanyName,
                        VATNumber = order.VATNumber,
                        EIK = order.EIK,
                        Custodian = order.Custodian
                    };
            }
        }

        public int Id { get; set; }

        public UserProfile User
        {
            set
            {
                var a = value;
            }
        }

        [Display(Name = "User")]
        public string UserId { get; set; }

        public string ByUser { get; set; }

        public DateTime MadeOn { get; set; }

        public bool IsPaid { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsFinalized { get; set; }

        public decimal? Total { get; set; }

        public string AdditionalDetails { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public DateTime OrderDate { get; set; }

        public string EmailAddress { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyName { get; set; }

        public string VATNumber { get; set; }

        public string EIK { get; set; }

        public string Custodian { get; set; }
    }
}