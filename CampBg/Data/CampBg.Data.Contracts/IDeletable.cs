namespace CampBg.Data.Contracts
{
    using System;

    public interface IDeletable
    {
        DateTime? DeletedOn { get; set; }

        bool IsDeleted { get; set; }
    }
}
