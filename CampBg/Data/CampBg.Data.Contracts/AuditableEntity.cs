namespace CampBg.Data.Contracts
{
    using System;

    public abstract class AuditableEntity : IAuditable
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
