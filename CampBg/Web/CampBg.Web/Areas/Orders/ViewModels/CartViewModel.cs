namespace CampBg.Web.Areas.Orders.ViewModels
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using CampBg.Common.ShoppingCart;
    using CampBg.Data;
    using CampBg.Data.Models;
    using CampBg.Data.Repositories.Contracts;
    using CampBg.Extensions;

    public class CartViewModel
    {
        private readonly IUowData data;

        public CartViewModel(IEnumerable<CartItem> items) : this(items, new UowData())
        {
        }

        public CartViewModel(IEnumerable<CartItem> items, IUowData data)
        {
            this.data = data;
            var cartItems = items as CartItem[] ?? items.ToArray();
            var products =
                this.data.Products.All()
                    .Include(x => x.PropertyValues)
                    .Include(x => x.PropertyValues.Select(z => z.Property))
                    .Include(x => x.Manufacturer)
                    .Include(x => x.ProductImages)
                    .Where(LinqBuilder.BuildOrExpression<Product, int>(x => x.Id, cartItems.Select(z => z.ProductId)))
                    .ToList();

            this.Items = cartItems.Select(
                pr =>
                {
                    var entityItem = products.First(x => x.Id == pr.ProductId);
                    var item = new CartItemViewModel(pr, entityItem);
                    return item;
                });
        }

        public IEnumerable<CartItemViewModel> Items { get; set; }

        public decimal Total
        {
            get
            {
                return this.Items.Sum(x => x.Price * x.Quantity);
            }
        }
    }
}