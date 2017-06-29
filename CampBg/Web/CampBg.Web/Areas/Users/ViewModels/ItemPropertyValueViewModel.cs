namespace CampBg.Web.Areas.Users.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class ItemPropertyValueViewModel
    {
        public static Expression<Func<PropertyValue, ItemPropertyValueViewModel>> FromPropertyValue
        {
            get
            {
                return pv => new ItemPropertyValueViewModel
                                 {
                                     Property = pv.Property.Name,
                                     Value = pv.Value
                                 };
            }
        }

        public static Expression<Func<PropertyValue, ItemPropertyValueViewModel>> FromPropertyValueEn
        {
            get
            {
                return pv => new ItemPropertyValueViewModel
                {
                    Property = pv.Property.NameEn,
                    Value = pv.ValueEn
                };
            }
        }

        public string Property { get; set; }

        public string Value { get; set; }
    }
}