namespace CampBg.Data.Models
{
    using CampBg.Data.Contracts;

    public class Newsletter : DeletableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Contents { get; set; }

        public bool AlreadySent { get; set; }
    }
}
