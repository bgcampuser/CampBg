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
                            CategoryId = product.Subcategory.Category.Id,
                            SubcategoryId = product.SubcategoryId.Value,
                            SubcategoryOptionId = product.SubcategoryOptionId,
                            IsPopular = product.IsPopular,
                            LastModified = product.ModifiedOn,
                            ManufacturerIdentificationNumber = product.ManufacturerIdentificationNumber,
                            CreatedById = product.CreatedById,
                            Properties = product.PropertyValues
                                                .AsQueryable()
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
        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        [Display(Name = "Подкатегория")]
        public int SubcategoryId { get; set; }

        [Display(Name = "Опция на подкатегория")]
        public int? SubcategoryOptionId { get; set; }

        [Display(Name = "В популярни")]
        public bool IsPopular { get; set; }

        public IEnumerable<PropertyValueViewModel> Properties { get; set; }

        [Display(Name = "Индиф. номер")]
        public string ManufacturerIdentificationNumber { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        
        [Display(Name = "Създаден от")]
        public string CreatedById { get; set; }

        [Display(Name = "Редактиран на")]
        public DateTime? LastModified { get; set; }
    }
}