namespace CampBg.Web.Areas.Orders.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CampBg.Common.ShoppingCart.Managers;
    using CampBg.Web.Areas.Orders.InputModels;
    using CampBg.Web.Areas.Orders.ViewModels;
    using CampBg.Web.Controllers;
    using CampBg.Web.Localization;

    public class CartController : BaseController
    {
        private CartManager cartManager;

        public CartManager CartManager
        {
            get
            {
                return this.cartManager ?? (this.cartManager = new CartManager(this.Session));
            }
        }

        public ActionResult Index()
        {
            var items = this.CartManager.Cart.Items;

//#if DEBUG
//            if (items.Count == 0)
//            {
//                this.CartManager.AddItem(19, 3, null);
//            }
//#endif

            var cartViewModel = new CartViewModel(items);

            return this.View(cartViewModel);
        }

        [HttpPost]
        public ActionResult AddToCart(ProductCartInputModel model)
        {
            var properties = this.GetProductProperties(model.SelectedProperties);
            this.CartManager.AddItem(model.ProductId, model.Quantity, properties);

            return this.Json("Success");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(ProductCartInputModel model)
        {
            var properties = this.GetProductProperties(model.SelectedProperties);
            this.CartManager.RemoveItem(model.ProductId, properties);

            return this.Json("Success");
        }

        [HttpPost]
        public ActionResult DecreaseQuantity(ProductCartInputModel model)
        {
            var properties = this.GetProductProperties(model.SelectedProperties);
            this.CartManager.DecreaseQuantity(model.ProductId, properties, 1);

            return this.Json("Success");
        }

        public ActionResult Confirm()
        {
            var items = this.CartManager.Cart.Items;

            var orderConfirmationViewModel = new OrderConfirmationViewModel
                                                 {
                                                     Cart = new CartViewModel(items),
                                                 };

            if (this.User.Identity.IsAuthenticated)
            {
                orderConfirmationViewModel.Addresses =
                    this.Data.Users.GetById(this.UserProfile.Id)
                        .DeliveryAddresses.AsQueryable().Where(x => !x.IsDeleted)
                        .Select(AddressViewModel.FromDeliveryAddress);
            }

            return this.View(orderConfirmationViewModel);
        }

        [HttpPost]
        public ActionResult Place(OrderConfirmationViewModel model)
        {
            model.Cart = new CartViewModel(this.CartManager.Cart.Items);
            if (this.UserProfile != null)
            {
                model.Addresses =
                    this.Data.Users.GetById(this.UserProfile.Id)
                        .DeliveryAddresses.AsQueryable()
                        .Where(x => !x.IsDeleted)
                        .Select(AddressViewModel.FromDeliveryAddress);
            }

            if (!this.User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(model.PhoneNumber))
                {
                    this.ModelState.AddModelError("PhoneNumber", Views.No_phone_number);
                    return this.View("Confirm", model);
                }

                if (string.IsNullOrEmpty(model.Name))
                {
                    this.ModelState.AddModelError("Name", Views.No_name);
                    return this.View("Confirm", model);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(model.PhoneNumber))
                {
                    model.PhoneNumber = this.UserProfile.PhoneNumber;
                }

                model.Name = this.UserProfile.UserName;
            }

            if (model.AddressId == null)
            {
                var addrIsEmpty = string.IsNullOrEmpty(model.Address);
                var cityIsEmpty = string.IsNullOrEmpty(model.City);
                var postalCodeIsEmpty = model.PostalCode <= 0;
                if (addrIsEmpty || cityIsEmpty || postalCodeIsEmpty)
                {
                    if (addrIsEmpty)
                    {
                        this.ModelState.AddModelError("Address", Views.No_address);
                    }

                    if (cityIsEmpty)
                    {
                        this.ModelState.AddModelError("City", Views.No_city);
                    }

                    if (postalCodeIsEmpty)
                    {
                        this.ModelState.AddModelError("PostalCode", Views.No_postal_code);
                    }

                    return this.View("Confirm", model);
                }
            }

            this.TempData["model"] = model;
            return this.RedirectToAction("Place", new { area = "Orders", controller = "Orders" });
        }

        private IEnumerable<KeyValuePair<int, int>> GetProductProperties(IEnumerable<PropertyValueInputModel> properties)
        {
            var result = properties != null
                             ? properties.Select(x => new KeyValuePair<int, int>(x.PropertyId, x.PropertyValueId))
                             : new HashSet<KeyValuePair<int, int>>();

            return result;
        }
    }
}