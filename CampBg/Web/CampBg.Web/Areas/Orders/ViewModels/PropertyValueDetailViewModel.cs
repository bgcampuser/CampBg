namespace CampBg.Web.Areas.Orders.ViewModels
{
    using CampBg.Common.ShoppingCart;
    using CampBg.Data.Models;

    public class PropertyValueDetailViewModel
    {
        public PropertyValueDetailViewModel()
        {
        }

        public PropertyValueDetailViewModel(ItemProperty cartProperty, PropertyValue entityPropertyValue)
        {
            this.PropertyId = cartProperty.PropertyId;
            this.PropertyValueId = cartProperty.PropertyValueId;
            this.PropertyName = entityPropertyValue.Property.Name;
            this.Value = entityPropertyValue.Value;
        }

        public int PropertyId { get; set; }

        public int PropertyValueId { get; set; }

        public string PropertyName { get; set; }

        public string Value { get; set; }
    }
}