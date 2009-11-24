using System;
using Jamaica.Repositories;
using Jamaica.Security;
using NHibernate;
using NHibernate.Criterion;

namespace Jamaica.NHibernate.Repositories
{
    public class SecurityPrincipalRepository : ISecurityPrincipalRepository
    {
        readonly ISession session;

        public SecurityPrincipalRepository(ISession session)
        {
            this.session = session;
        }

        public ISecurityPrincipal GetByNameAndHash(string username, string hash)
        {
            return session.CreateCriteria<User>()
                .Add(Restrictions.Eq("Name", username))
                .Add(Restrictions.Eq("Hash", hash))
                .UniqueResult<User>();
        }
    }
}