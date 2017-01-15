
using System.Drawing;

namespace CampBg.Web.Areas.Orders.Controllers
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Web.Mvc;

    using CampBg.Common.ShoppingCart.Managers;
    using CampBg.Data.Models;
    using CampBg.Extensions;
    using CampBg.Web.Areas.Orders.ViewModels;
    using CampBg.Web.Controllers;
    using CampBg.Web.Localization;

    using ResourceEmails = CampBg.Web.Localization.EmailTemplates;

    public class OrdersController : BaseController
    {
        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Orders", new { area = "Users" });
            }

            return this.RedirectToAction("Login", "Account", new { area = string.Empty });
        }

        [HttpGet]
        public ActionResult Place()
        {
            var model = (OrderConfirmationViewModel)this.TempData["model"];
            var httpSessionStateBase = this.HttpContext.Session;
            var cartManager = new CartManager(httpSessionStateBase);
            var shoppingCart = cartManager.GetShoppingCart();

            var products =
                this.Data.Products.All()
                    .Include(x => x.PropertyValues)
                    .Where(
                        LinqBuilder.BuildOrExpression<Product, int>(
                            x => x.Id,
                            shoppingCart.Items.Select(x => x.ProductId)))
                    .ToList();

            var order = new Order
                            {
                                OrderItems = shoppingCart.Items.Select(
                                    oi =>
                                    {
                                        var product = products.First(pr => pr.Id == oi.ProductId);
                                        var orderItem = new OrderItem
                                                            {
                                                                ProductId = oi.ProductId,
                                                                Price = product.Price,
                                                                Quantity = oi.Quantity,
                                                                PropertyValues =
                                                                    product.PropertyValues.Where(
                                                                        pv =>
                                                                        oi.Properties.Any(prop => prop.PropertyValueId == pv.Id))
                                                                    .ToList()
                                                            };
                                        return orderItem;
                                    }).ToList(),
                                User = this.UserProfile,
                                Custodian = model.Custodian,
                                PhoneNumber = model.PhoneNumber,
                                PaymentMethod = this.GetPaymentMethod(model.PaymentMethod),
                                AdditionalDetails = model.AdditionalDetails,
                                OrderedBy = this.UserProfile != null ? this.UserProfile.UserName : model.Name,
                                EmailAddress = this.UserProfile != null ? this.UserProfile.Email : model.EmailAddress,
                            };

            if (model.Receipt)
            {
                order.CompanyAddress = model.CompanyAddress;
                order.CompanyName = model.CompanyName;
                order.VATNumber = model.VATNumber;
                order.EIK = model.EIK;
            }

            if (model.AddressId != null)
            {
                order.DeliveryAddressId = model.AddressId.Value;
            }
            else
            {
                order.DeliveryAddress = new DeliveryAddress
                                            {
                                                Address = model.Address,
                                                City = model.City,
                                                PostalCode = model.PostalCode
                                            };
            }

            this.Data.Orders.Add(order);
            this.Data.SaveChanges();

            cartManager.EmptyCart();

            string emailAddress;

            if (!string.IsNullOrEmpty(model.EmailAddress))
            {
                emailAddress = model.EmailAddress;
            }
            else
            {
                emailAddress = this.UserProfile.Email;
            }

            var emailBody = this.getOrderEmailBody(order, model.Receipt);

            if (!string.IsNullOrEmpty(emailAddress))
            {
                try
                {
                    this.MailSender.SendMail(
                        emailAddress,
                        "Order successfull",
                        emailBody);
                }
                catch (Exception ex)
                {
                    // TODO: Add error handling
                }
            }

            var orderPlacedViewModel = new OrderPlacedViewModel { Id = order.Id };

            return this.View("Confirmed", orderPlacedViewModel);
        }

        private string getOrderEmailBody(Order order, bool addReceipt)
        {
            var emailAddress = ConfigurationManager.AppSettings["contactEmail"];
            var phoneNumber = ConfigurationManager.AppSettings["phoneNumber"];
            var isBg = Thread.CurrentThread.CurrentCulture.Name == "bg-BG";
            var paymentInfo = this.paymentInfoString(order, addReceipt);
            var deliveryInfo = this.deliveryInfoString(order);
            var orderInfo = this.orderInfo(order, isBg);
            var total = order.OrderItems.Sum(x => x.Price);
            var paymentMethodString = this.getPaymentMethodString(order.PaymentMethod);
            var deliveryMethod = "EKONT";
            var paymentDetailsString = addReceipt ? ResourceEmails.Payment_information : string.Empty;
            var emailBody = string.Format(
                EmailTemplates.OrderBody,
                emailAddress,
                phoneNumber,
                order.Id,
                order.CreatedOn.ToString(),
                paymentDetailsString,
                paymentInfo,
                paymentMethodString,
                deliveryInfo,
                deliveryMethod,
                orderInfo,
                total);

            return emailBody;
        }

        private string getPaymentMethodString(PaymentMethod paymentMethod)
        {
            string paymentMethodString;
            switch (paymentMethod)
            {
                case PaymentMethod.BankTransfer:
                    paymentMethodString = ResourceEmails.Payment_method_bank;
                    break;
                case PaymentMethod.UponDelivery:
                    paymentMethodString = ResourceEmails.Payment_method_delivery;
                    break;
                default:
                    paymentMethodString = ResourceEmails.Payment_method_unknown;
                    break;
            }

            return paymentMethodString;
        }

        private string orderInfo(Order order, bool isBg)
        {
            var sb = new StringBuilder();
            foreach (var orderItem in order.OrderItems)
            {
                var productName = isBg ? orderItem.Product.Name : orderItem.Product.NameEn;

                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", productName, orderItem.Quantity, orderItem.Price);
            }

            return sb.ToString();
        }

        private string deliveryInfoString(Order order)
        {
            var sb = new StringBuilder();
            if (order.DeliveryAddress != null)
            {
                sb.Append(order.DeliveryAddress.Address);
                sb.Append("<br/>");
                sb.Append(order.DeliveryAddress.City);
                sb.Append("<br/>");
                sb.Append(order.DeliveryAddress.PostalCode);
                sb.Append("<br/>");
                sb.Append(order.DeliveryAddress.UserProfile != null ? order.DeliveryAddress.UserProfile.PhoneNumber : string.Empty);
                sb.Append("<br/>");
                sb.Append(order.DeliveryAddress.UserProfile != null ? order.DeliveryAddress.UserProfile.Email : string.Empty);
                sb.Append("<br/>");
            }


            return sb.ToString();
        }

        private string paymentInfoString(Order order, bool hasReceipt)
        {
            var sb = new StringBuilder();

            if (hasReceipt)
            {
                var custodian = ResourceEmails.Custodian;
                var custodianInfo = string.Format(custodian, order.Custodian);
                sb.Append("<p>");
                sb.Append(custodianInfo);
                sb.Append("</p>");

                var companyName = ResourceEmails.Company_name;
                var companyNameInfo = string.Format(companyName, order.CompanyName);
                sb.Append("<p>");
                sb.Append(companyNameInfo);
                sb.Append("</p>");

                var companyAddress = ResourceEmails.Company_address;
                var companyAddressInfo = string.Format(companyAddress, order.CompanyAddress);
                sb.Append("<p>");
                sb.Append(companyAddressInfo); 
                sb.Append("</p>");

                var vatNum = ResourceEmails.Vat_number;
                var vatInfo = string.Format(vatNum, order.VATNumber);
                sb.Append("<p>");
                sb.Append(vatInfo); 
                sb.Append("</p>");
                
                var eik = ResourceEmails.EIK;
                var eikInfo = string.Format(eik, order.EIK);
                sb.Append("<p>");
                sb.Append(eikInfo); 
                sb.Append("</p>");
            }

            return sb.ToString();
        }

        public ActionResult Confirmed()
        {
            return this.View();
        }

        private PaymentMethod GetPaymentMethod(PaymentMethodViewModel vm)
        {
            if (vm == PaymentMethodViewModel.BankTransfer)
            {
                return PaymentMethod.BankTransfer;
            }
            else
            {
                return PaymentMethod.UponDelivery;
            }
        }
    }
}