namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class ProductImageViewModel
    {
        public static Expression<Func<ProductImage, ProductImageViewModel>> FromProductImage
        {
            get
            {
                return image => new ProductImageViewModel
                                    {
                                        Id = image.Id,
                                        Location = image.Location,
                                        Thumbnail = image.ThumbnailLocation,
                                        ProductId = image.ProductId
                                    };
            }
        }

        public int? ProductId { get; set; }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Location { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Thumbnail { get; set; }
    }
}