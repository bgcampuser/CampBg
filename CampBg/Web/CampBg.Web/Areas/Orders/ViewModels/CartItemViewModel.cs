namespace CampBg.Web.Areas.Orders.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using CampBg.Common.ShoppingCart;
    using CampBg.Data.Models;

    public class CartItemViewModel
    {
        public CartItemViewModel()
        {
        }

        public CartItemViewModel(CartItem item, Product productEntity)
        {
            this.Id = productEntity.Id;
            this.Name = productEntity.Name;
            this.Quantity = item.Quantity;
            this.Price = productEntity.Price;
            this.Manufacturer = productEntity.Manufacturer.Name;
            var defaultImage = productEntity.ProductImages.FirstOrDefault();

            this.ImageUrl = defaultImage != null
                                ? defaultImage.Location
                                : Resources.GlobalConstants.PlaceholderImage;

            this.ThumbUrl = defaultImage != null
                                ? defaultImage.ThumbnailLocation
                                : Resources.GlobalConstants.PlaceholderThumbnailImage;

            var itemProperties = item.Properties;
            this.Properties = itemProperties != null
                                  ? itemProperties.Select(
                                      pr =>
                                          {
                                              var propertyEntity =
                                                  productEntity.PropertyValues.First(x => x.Id == pr.PropertyValueId);
                                              var property = new PropertyValueDetailViewModel(pr, propertyEntity);
                                              return property;
                                          })
                                  : new HashSet<PropertyValueDetailViewModel>();
        }

        public string ThumbUrl { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<PropertyValueDetailViewModel> Properties { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Manufacturer { get; set; }
    }
}