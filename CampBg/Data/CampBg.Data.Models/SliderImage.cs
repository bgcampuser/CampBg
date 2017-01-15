namespace CampBg.Data.Models
{
    using System;

    using CampBg.Data.Contracts;

    public class SliderImage : IDeletable, IAuditable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Url { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
