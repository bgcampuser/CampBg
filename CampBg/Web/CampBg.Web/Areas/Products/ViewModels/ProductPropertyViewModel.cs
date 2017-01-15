namespace CampBg.Web.Areas.Products.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class ProductPropertyViewModel
    {
        public static Expression<Func<PropertyValue, ProductPropertyViewModel>> FromPropertyValue
        {
            get
            {
                return propValue => new ProductPropertyViewModel
                                        {
                                            PropertyId = propValue.PropertyId,
                                            Name = propValue.Property.Name
                                        };
            }
        }

        public int PropertyId { get; set; }

        public string Name { get; set; }

        public IEnumerable<PropertyValueViewModel> Values { get; set; }
    }
}