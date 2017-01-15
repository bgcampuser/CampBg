namespace CampBg.Data.Repositories
{
    using System.Data.Entity;
    using System.Linq;

    using CampBg.Data.Models;
    using CampBg.Data.Repositories.Contracts;

    public class UsersRepository : GenericRepository<UserProfile>, IUsersRepository
    {
        public UsersRepository(DbContext context) : base(context)
        {
        }

        public UserProfile GetByUsername(string username)
        {
            return this.All().FirstOrDefault(x => x.UserName == username);
        }

        public UserProfile GetById(string id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<UserProfile> AllWithDeleted()
        {
            return this.All();
        }

        public void Delete(UserProfile entity)
        {
            entity.Email += "camp.bg_inactive";
            entity.UserName += "camp.bg_inactive";
            base.Delete(entity);
        }
    }
}
