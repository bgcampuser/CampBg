namespace CampBg.Data
{
    using System.Data.Entity;
    using System.Linq;

    using CampBg.Data.Contracts;

    public class GenericDeletableRepository<T> : GenericRepository<T>, IDeletableRepository<T>
        where T : class, IDeletable
    {
        public GenericDeletableRepository(DbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return this.DbSet.AsQueryable().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.DbSet.AsQueryable();
        }
    }
}
