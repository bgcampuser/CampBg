namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class PropertyViewModel
    {
        public static Expression<Func<Property, PropertyViewModel>> FromProperty
        {
            get
            {
                return
                    property => new PropertyViewModel
                    {
                        Id = property.Id,
                        Name = property.Name,
                        NameEn = property.NameEn
                    };
            }
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string NameEn { get; set; }
    }
}