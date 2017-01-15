namespace CampBg.Web.Areas.Products.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class ManufacturerViewModel
    {
        public static Expression<Func<Manufacturer, ManufacturerViewModel>> FromManufacturer
        {
            get
            {
                return x => new ManufacturerViewModel { Id = x.Id, Name = x.Name };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}