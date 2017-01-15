namespace CampBg.Web.ViewModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class ProductViewModel
    {
        public static Expression<Func<Product, ProductViewModel>> FromProduct
        {
            get
            {
                return prod => new ProductViewModel
                                   {
                                       Id = prod.Id,
                                       Name = prod.Name,
                                       Price = prod.Price,
                                       ImageUrl = prod.ProductImages.FirstOrDefault().Location,
                                       Manufacturer = prod.Manufacturer.Name
                                   };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Manufacturer { get; set; }

        public string ImageUrl { get; set; }
    }
}