namespace CampBg.Common.ShoppingCart.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using CampBg.Common.ShoppingCart;

    public class CartManager
    {
        private const string SessionSaveKey = "cart";

        private readonly HttpSessionStateBase session;

        private Cart cart;

        public CartManager(HttpSessionStateBase session)
        {
            this.session = session;
        }

        public Cart Cart
        {
            get
            {
                if (this.cart == null)
                {
                    this.cart = (Cart)this.session[SessionSaveKey] ?? new Cart();
                }

                return this.cart;
            }
        }

        public void AddItem(int productId, int quantity, IEnumerable<KeyValuePair<int, int>> properties)
        {
            if (properties == null)
            {
                properties = new KeyValuePair<int, int>[0];
            }

            var product = this.GetCartItem(productId, properties);

            if (quantity != 0)
            {
                if (product != null)
                {
                    this.Cart.IncreaseQuantity(product, quantity);
                }
                else
                {
                    this.Cart.Add(new CartItem(productId, quantity, properties));
                }
            }

            this.session.Add(SessionSaveKey, this.Cart);
        }

        public Cart GetShoppingCart()
        {
            return this.Cart;
        }

        public void EmptyCart()
        {
            this.session[SessionSaveKey] = null;
        }

        public void RemoveItem(int productId, IEnumerable<KeyValuePair<int, int>> properties)
        {
            var cartItem = this.GetCartItem(productId, properties);
            this.Cart.Remove(cartItem);
            this.session.Add(SessionSaveKey, this.Cart);
        }

        public void DecreaseQuantity(int productId, IEnumerable<KeyValuePair<int, int>> properties, int quantity)
        {
            var cartItem = this.GetCartItem(productId, properties);
            this.Cart.DecreaseQuantity(cartItem, quantity);
            this.session.Add(SessionSaveKey, this.Cart);
        }

        private CartItem GetCartItem(int productId, IEnumerable<KeyValuePair<int, int>> properties)
        {
            var cartItem =
                this.Cart.Items.FirstOrDefault(
                    x =>
                    x.ProductId == productId
                    && x.Properties.All(p => properties.Any(m => m.Key == p.PropertyId && m.Value == p.PropertyValueId)));

            return cartItem;
        }
    }
}
