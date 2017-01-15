namespace CampBg.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    public static class EnumExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            return from Enum e in Enum.GetValues(enumType)
                   select new SelectListItem
                   {
                       Selected = e.Equals(enumValue),
                       Text = enumType.GetMember(e.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName(),
                       Value = e.ToString()
                   };
        }

        public static string ToDescription(this Enum value)
        {
            var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        } 
    }
}
