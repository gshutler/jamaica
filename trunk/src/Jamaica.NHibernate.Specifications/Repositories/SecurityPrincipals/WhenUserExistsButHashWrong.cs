using System;
using Jamaica.NHibernate.Repositories;
using Jamaica.Security;
using NUnit.Framework;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.NHibernate.Specifications.Repositories.SecurityPrincipals
{
    public class WhenUserExistsButHashWrong : IntegrationSpecification
    {
        ISecurityPrincipal securityPrincipal;
        User user;

        protected override void Given()
        {
            using (var transaction = session.BeginTransaction())
            {
                user = new User("NathanTyson");

                user.SetPassword("forest");

                session.Save(user);
                transaction.Commit();
            }

            session.Clear();
        }

        protected override void When()
        {
            securityPrincipal = Subject<SecurityPrincipalRepository>().GetByNameAndHash(user.Name, "wrong_hash");
        }

        [Then]
        public void SecurityPrincipalIsNull()
        {
            Verify(securityPrincipal, Is.Null);
        }
    }
}