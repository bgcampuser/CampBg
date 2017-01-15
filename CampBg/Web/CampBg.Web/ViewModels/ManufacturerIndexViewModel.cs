namespace CampBg.Web.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class ManufacturerIndexViewModel
    {
        public static Expression<Func<Manufacturer, ManufacturerIndexViewModel>> FromManufacturer
        {
            get
            {
                return mf => new ManufacturerIndexViewModel
                                 {
                                     Id = mf.Id,
                                     Image = mf.Logo,
                                     ManufacturerName = mf.Name
                                 };
            }
        }

        public int Id { get; set; }

        public string Image { get; set; }

        public string ManufacturerName { get; set; }
    }
}