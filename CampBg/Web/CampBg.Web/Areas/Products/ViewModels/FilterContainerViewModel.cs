namespace CampBg.Web.Areas.Products.ViewModels
{
    using System.Collections.Generic;

    public class FilterContainerViewModel
    {
        public IEnumerable<ProductPropertyViewModel> PropertyFilter { get; set; }

        public IEnumerable<ManufacturerViewModel> ManufacturerFilter { get; set; }

        public PriceViewModel PriceFilter { get; set; }
    }
}