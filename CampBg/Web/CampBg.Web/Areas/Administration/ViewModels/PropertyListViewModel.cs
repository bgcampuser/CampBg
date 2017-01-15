namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PropertyListViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}