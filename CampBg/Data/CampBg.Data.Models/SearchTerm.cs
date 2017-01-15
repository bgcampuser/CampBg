namespace CampBg.Data.Models
{
    using CampBg.Data.Contracts;

    public class SearchTerm : DeletableEntity
    {
        public int Id { get; set; }

        public string Term { get; set; }

        public int Count { get; set; }
    }
}