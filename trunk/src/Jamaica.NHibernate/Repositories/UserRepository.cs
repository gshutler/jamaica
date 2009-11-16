using System;
using Jamaica.Repositories;
using Jamaica.Security;

namespace Jamaica.NHibernate.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User GetByUsernameAndHash(string username, string hash)
        {
            throw new NotImplementedException();
        }
    }
}