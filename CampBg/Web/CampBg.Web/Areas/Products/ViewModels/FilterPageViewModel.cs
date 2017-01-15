namespace CampBg.Web.Areas.Products.ViewModels
{
    using System.Collections.Generic;

    using CampBg.Web.ViewModels;

    public class FilterPageViewModel
    {
        public FilterContainerViewModel Filters { get; set; }

        public IEnumerable<ProductListViewModel> InitialProducts { get; set; }
    }
}