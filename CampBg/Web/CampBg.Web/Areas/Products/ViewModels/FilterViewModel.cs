namespace CampBg.Web.Areas.Products.ViewModels
{
    using System.Collections.Generic;

    public class FilterViewModel
    {
        public FilterViewModel()
        {
            this.Manufacturers = new HashSet<int>();
            this.SelectedProperties = new HashSet<PropertyValueViewModel>();
        }

        public IEnumerable<int> Manufacturers { get; set; }

        public IEnumerable<PropertyValueViewModel> SelectedProperties { get; set; }

        public PriceFilterViewModel PriceFilter { get; set; }
    }

    public class PriceFilterViewModel
    {
        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }
    }
}