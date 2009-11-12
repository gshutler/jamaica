using Jamaica.Security;

namespace Jamaica.Repositories
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        void Add(User user);
    }
}