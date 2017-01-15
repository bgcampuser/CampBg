namespace CampBg.Web.ViewModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using CampBg.Data.Models;
    using CampBg.Web.Areas.Products.ViewModels;

    public class ProductListViewModel
    {
        private ProductImageViewModel image;

        public static Expression<Func<Product, ProductListViewModel>> FromProduct
        {
            get
            {
                return
                    prod =>
                    new ProductListViewModel
                        {
                            Id = prod.Id,
                            Name = prod.Name,
                            Price = prod.Price,
                            Manufacturer = prod.Manufacturer.Name,
                            Image =
                                prod.ProductImages.AsQueryable()
                                .Where(x => !x.IsDeleted && x.IsDefault)
                                .Select(ProductImageViewModel.FromProductImage)
                                .FirstOrDefault()
                        };
            }
        }

        public static Expression<Func<Product, ProductListViewModel>> FromProductEn
        {
            get
            {
                return
                    prod =>
                    new ProductListViewModel
                    {
                        Id = prod.Id,
                        Name = prod.NameEn,
                        Price = prod.Price,
                        Manufacturer = prod.Manufacturer.Name,
                        Image =
                            prod.ProductImages.AsQueryable()
                            .Where(x => !x.IsDeleted && x.IsDefault)
                            .Select(ProductImageViewModel.FromProductImage)
                            .FirstOrDefault()
                    };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public decimal Price { get; set; }

        public ProductImageViewModel Image
        {
            get
            {
                if (this.image == null)
                {
                    return new ProductImageViewModel
                               {
                                   Location = Resources.GlobalConstants.PlaceholderImage,
                                   IsDefault = true
                               };
                }

                return this.image;
            }

            private set
            {
                this.image = value;
            }
        }
    }
}