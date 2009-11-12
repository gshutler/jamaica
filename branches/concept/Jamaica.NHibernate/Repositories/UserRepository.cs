using System;
using Jamaica.Repositories;
using Jamaica.Security;
using NHibernate;
using NHibernate.Criterion;

namespace Jamaica.NHibernate.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession session;

        public UserRepository(ISession session)
        {
            this.session = session;
        }

        public User GetByUsername(string username)
        {
            return session.CreateCriteria<User>()
                .Add(Restrictions.Eq("Username", username))
                .UniqueResult<User>();
        }

        public void Add(User user)
        {
            session.Save(user);
            session.Transaction.Commit();
        }
    }
}