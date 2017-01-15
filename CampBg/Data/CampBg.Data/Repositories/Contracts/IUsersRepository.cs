namespace CampBg.Data.Repositories.Contracts
{
    using CampBg.Data.Contracts;
    using CampBg.Data.Models;

    public interface IUsersRepository : IDeletableRepository<UserProfile>
    {
        UserProfile GetByUsername(string username);

        UserProfile GetById(string id);
    }
}
