namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class OrderItemPropertyViewModel
    {
        public static Expression<Func<PropertyValue, OrderItemPropertyViewModel>> FromPropertyValue
        {
            get
            {
                return pv => new OrderItemPropertyViewModel
                                 {
                                     Id = pv.Id,
                                     PropertyId = pv.PropertyId,
                                     PropertyValue = pv.Value,
                                     PropertyValueId = pv.Id,
                                     Property = pv.Property.Name
                                 };
            }
        }

        public int Id { get; set; }

        public int PropertyValueId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PropertyValue { get; set; }

        public int PropertyId { get; set; }

        public string Property { get; set; }
    }
}