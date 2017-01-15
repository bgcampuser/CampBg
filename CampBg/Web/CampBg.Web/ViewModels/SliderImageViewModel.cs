namespace CampBg.Web.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class SliderImageViewModel
    {
        public static Expression<Func<SliderImage, SliderImageViewModel>> FromSliderImage
        {
            get
            {
                return si => new SliderImageViewModel
                                 {
                                     Location = si.Location,
                                     Url = si.Url
                                 };
            }
        }

        public string Location { get; set; }

        public string Url { get; set; }
    }
}