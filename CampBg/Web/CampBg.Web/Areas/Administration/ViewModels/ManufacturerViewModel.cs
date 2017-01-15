namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class ManufacturerViewModel
    {
        public static Expression<Func<Manufacturer, ManufacturerViewModel>> FromManufacturer
        {
            get
            {
                return
                    m =>
                    new ManufacturerViewModel
                        {
                            Id = m.Id, Name = m.Name, Logo = m.Logo, IsInTopMenu = m.IsInTopMenu
                        };
            }
        }
        
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public string Logo { get; set; }

        public bool IsInTopMenu { get; set; }
    }
}