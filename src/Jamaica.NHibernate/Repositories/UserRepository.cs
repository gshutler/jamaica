using System;
using Jamaica.Repositories;
using Jamaica.Security;
using NHibernate;
using NHibernate.Criterion;

namespace Jamaica.NHibernate.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly ISession session;

        public UserRepository(ISession session)
        {
            this.session = session;
        }

        public User GetByUsernameAndHash(string username, string hash)
        {
            return session.CreateCriteria<User>()
                .Add(Restrictions.Eq("Username", username))
                .Add(Restrictions.Eq("Hash", hash))
                .UniqueResult<User>();
        }
    }
}