namespace CampBg.Web.Areas.Orders.ViewModels
{
    using System.Collections.Generic;

    using CampBg.Web.Areas.Orders.InputModels;

    public class ProductCartInputModel
    {
        public int ProductId { get; set; }

        public IEnumerable<PropertyValueInputModel> SelectedProperties { get; set; }

        public int Quantity { get; set; }
    }
}