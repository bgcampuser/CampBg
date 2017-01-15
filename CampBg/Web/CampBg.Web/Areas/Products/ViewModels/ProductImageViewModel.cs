namespace CampBg.Web.Areas.Products.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class ProductImageViewModel
    {
        public static Expression<Func<ProductImage, ProductImageViewModel>> FromProductImage
        {
            get
            {
                return img => new ProductImageViewModel
                                 {
                                     Id = img.Id,
                                     Location = img.Location,
                                     IsDefault = img.IsDefault,
                                     Thumbnail = img.ThumbnailLocation
                                 };
            }
        }

        public int Id { get; set; }

        public string Location { get; set; }

        public string Thumbnail { get; set; }

        public bool IsDefault { get; set; }
    }
}