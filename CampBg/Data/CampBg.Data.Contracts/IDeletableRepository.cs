namespace CampBg.Data.Contracts
{
    using System.Linq;

    public interface IDeletableRepository<T> : IRepository<T> 
        where T : class, IDeletable
    {
        IQueryable<T> AllWithDeleted();
    }
}
