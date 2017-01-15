namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using CampBg.Data.Models;

    [Bind(Exclude = "Properties")]
    public class ProductViewModel
    {
        public static Expression<Func<Product, ProductViewModel>> FromProduct
        {
            get
            {
                return
                    product =>
                    new ProductViewModel
                        {
                            Id = product.Id,
                            Name = product.Name,
                            NameEn = product.NameEn,
                            Price = product.Price,
                            Description = product.Description,
                            DescriptionEn = product.DescriptionEn,
                            ManufacturerId = product.ManufacturerId,
                            SubcategoryOptionId = product.SubcategoryOptionId,
                            IsPopular = product.IsPopular,
                            ManufacturerIdentificationNumber = product.ManufacturerIdentificationNumber,
                            Properties =
                                product.PropertyValues.AsQueryable()
                                .Select(PropertyValueViewModel.FromPropertyValue)
                        };
            }
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Име на английски")]
        public string NameEn { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [DataType(DataType.Html)]
        [AllowHtml]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [DataType(DataType.Html)]
        [AllowHtml]
        [Display(Name = "Описание на английски")]
        public string DescriptionEn { get; set; }

        [Display(Name = "Производител")]
        public int ManufacturerId { get; set; }

        public int SubcategoryOptionId { get; set; }

        [Display(Name = "В популярни")]
        public bool IsPopular { get; set; }

        public IEnumerable<PropertyValueViewModel> Properties { get; set; }

        public string ManufacturerIdentificationNumber { get; set; }
    }
}