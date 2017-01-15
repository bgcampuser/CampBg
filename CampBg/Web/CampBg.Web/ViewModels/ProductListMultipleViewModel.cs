namespace CampBg.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductListMultipleViewModel
    {
        [UIHint("HomePageItemViewModel")]
        public IEnumerable<ProductListViewModel> Products { get; set; }

        public int TotalPages { get; set; }
    }
}