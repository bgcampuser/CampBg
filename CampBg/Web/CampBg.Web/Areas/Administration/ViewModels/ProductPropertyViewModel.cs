namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class ProductPropertyViewModel
    {
        public static Expression<Func<PropertyValue, ProductPropertyViewModel>> FromPropertyValue
        {
            get
            {
                return pv => new ProductPropertyViewModel
                {
                    Id = pv.Id,
                    PropertyName = pv.Property.Name,
                    Value = pv.Value,
                    ValueEn = pv.ValueEn,
                    PropertyId = pv.PropertyId
                };
            }
        }

        public int Id { get; set; }

        public int PropertyId { get; set; }

        public string PropertyName { get; set; }

        public string Value { get; set; }

        public string ValueEn { get; set; }
    }
}