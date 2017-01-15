namespace CampBg.Common.ShoppingCart
{
    using System.Collections.Generic;
    using System.Linq;

    public class CartItem
    {
        public CartItem(int productId, int quantity, IEnumerable<KeyValuePair<int, int>> properties)
        {
            this.ProductId = productId;
            var keyValuePairs = properties == null
                                    ? new KeyValuePair<int, int>[0]
                                    : properties as KeyValuePair<int, int>[] ?? properties.ToArray();
            this.Properties = keyValuePairs.Any()
                                  ? keyValuePairs.Select(
                                      x => new ItemProperty { PropertyId = x.Key, PropertyValueId = x.Value })
                                  : new HashSet<ItemProperty>();

            this.Quantity = quantity;
        }

        public int ProductId { get; set; }

        public IEnumerable<ItemProperty> Properties { get; set; }

        public int Quantity { get; set; }
    }
}