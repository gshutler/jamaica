using Jamaica.Security;

namespace Jamaica.Repositories
{
    public interface IUserRepository
    {
        User GetByUsernameAndHash(string username, string hash);
    }
}