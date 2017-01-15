namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class SliderImageViewModel
    {
        public static Expression<Func<SliderImage, SliderImageViewModel>> FromSliderImage
        {
            get
            {
                return
                    image =>
                    new SliderImageViewModel
                        {
                            Id = image.Id,
                            IsActive = image.IsActive,
                            Name = image.Name,
                            Location = image.Location,
                            Url = image.Url
                        };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsActive { get; set; }

        [ScaffoldColumn(false)]
        public string Location { get; set; }
    }
}