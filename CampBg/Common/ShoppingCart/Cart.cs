namespace CampBg.Common.ShoppingCart
{
    using System;
    using System.Collections.Generic;

    public class Cart
    {
        public Cart()
        {
            this.Items = new HashSet<CartItem>();
        }

        public ICollection<CartItem> Items { get; set; }

        public void Add(CartItem item)
        {
            this.Items.Add(item);
        }

        public void DecreaseQuantity(CartItem item, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException();
            }

            item.Quantity -= quantity;

            if (item.Quantity <= 0)
            {
                this.Remove(item);
            }
        }

        public void IncreaseQuantity(CartItem item, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException();
            }

            item.Quantity += quantity;
        }

        public void Remove(CartItem item)
        {
            this.Items.Remove(item);
        }
    }
}
