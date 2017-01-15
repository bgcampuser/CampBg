namespace CampBg.Web.Areas.Products.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using CampBg.Data.Models;
    using CampBg.Web.ViewModels;

    using Resources;

    public class ProductDetailsViewModel
    {
        private const int RelatedProductsCount = 4;

        public static Expression<Func<Product, ProductDetailsViewModel>> FromProduct
        {
            get
            {
                return prod => new ProductDetailsViewModel
                                   {
                                       Id = prod.Id,
                                       Name = prod.Name,
                                       Price = prod.Price,
                                       Description = prod.Description,
                                       Images = prod.ProductImages
                                                   .Where(x => !x.IsDeleted)
                                                   .AsQueryable()
                                                   .Select(ProductImageViewModel.FromProductImage),
                                       Properties = prod.PropertyValues
                                                   .Where(x => !x.IsDeleted)
                                                   .AsQueryable()
                                                   .GroupBy(x => x.Property)
                                                   .Select(x => new ProductPropertyViewModel
                                                                   {
                                                                       PropertyId = x.Key.Id,
                                                                       Name = x.Key.Name,
                                                                       Values = x.Select(v => new PropertyValueViewModel
                                                                                                {
                                                                                                    Id = v.Id,
                                                                                                    Value = v.Value
                                                                                                })
                                                                   }),
                                       Manufacturer = prod.Manufacturer.Name,
                                       RelatedProducts = prod.RelatedProducts
                                                            .AsQueryable()
                                                            .Where(x => !x.IsDeleted)
                                                            .Take(RelatedProductsCount)
                                                            .Select(ProductListViewModel.FromProduct),
                                       SubcategoryOption = prod.SubcategoryOption.Name,
                                       Subcategory = prod.SubcategoryOption.Subcategory.Name,
                                       Category = prod.SubcategoryOption.Subcategory.Category.Name
                                   };
            }
        }

        public static Expression<Func<Product, ProductDetailsViewModel>> FromProductEn
        {
            get
            {
                return prod => new ProductDetailsViewModel
                {
                    Id = prod.Id,
                    Name = prod.NameEn,
                    Price = prod.Price,
                    Description = prod.DescriptionEn,
                    Images = prod.ProductImages
                                .Where(x => !x.IsDeleted)
                                .AsQueryable()
                                .Select(ProductImageViewModel.FromProductImage),
                    Properties = prod.PropertyValues
                                .Where(x => !x.IsDeleted)
                                .AsQueryable()
                                .GroupBy(x => x.Property)
                                .Select(x => new ProductPropertyViewModel
                                {
                                    PropertyId = x.Key.Id,
                                    Name = x.Key.Name,
                                    Values = x.Select(v => new PropertyValueViewModel
                                    {
                                        Id = v.Id,
                                        Value = v.Value
                                    })
                                }),
                    Manufacturer = prod.Manufacturer.Name,
                    RelatedProducts = prod.RelatedProducts
                                         .AsQueryable()
                                         .Where(x => !x.IsDeleted)
                                         .Take(RelatedProductsCount)
                                         .Select(ProductListViewModel.FromProductEn),
                    SubcategoryOption = prod.SubcategoryOption.NameEn,
                    Subcategory = prod.SubcategoryOption.Subcategory.NameEn,
                    Category = prod.SubcategoryOption.Subcategory.Category.NameEn
                };
            }
        }

        public int Id { get; set; }

        public int ArticleId
        {
            get
            {
                return this.Id + 10000;
            }
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Manufacturer { get; set; }

        public IEnumerable<ProductImageViewModel> Images { get; set; }

        public IEnumerable<ProductPropertyViewModel> Properties { get; set; }

        public IEnumerable<ProductListViewModel> RelatedProducts { get; set; }

        public string DefaultImageLocation
        {
            get
            {
                var defaultImage = this.Images.FirstOrDefault(img => img.IsDefault);

                if (defaultImage != null)
                {
                    return defaultImage.Location;
                }

                var firstImage = this.Images.FirstOrDefault();

                return firstImage == null ? GlobalConstants.PlaceholderImage : firstImage.Location;
            }
        }

        public string Category { get; set; }

        public string Subcategory { get; set; }

        public string SubcategoryOption { get; set; }
    }
}