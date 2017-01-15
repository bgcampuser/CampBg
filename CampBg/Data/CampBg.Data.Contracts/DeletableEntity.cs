namespace CampBg.Data.Contracts
{
    using System;

    public abstract class DeletableEntity : AuditableEntity, IDeletable
    {
        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
