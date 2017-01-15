namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class PropertyValueViewModel
    {
        public static Expression<Func<PropertyValue, PropertyValueViewModel>> FromPropertyValue
        {
            get
            {
                return
                    pv =>
                    new PropertyValueViewModel
                        {
                            PropertyId = pv.PropertyId,
                            PropertyValueId = pv.Id,
                            Property = pv.Property.Name,
                            Value = pv.Value
                        };
            }
        }
        
        public int PropertyId { get; set; }

        public string Property { get; set; }

        public int PropertyValueId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }
    }
}