namespace CampBg.Web.ViewModels
{
    using System.Collections.Generic;

    public class IndexPageViewModel
    {
        public IndexPageViewModel()
        {
            this.SliderImages = new HashSet<SliderImageViewModel>();
        }

        public IEnumerable<SliderImageViewModel> SliderImages { get; set; }
    }
}