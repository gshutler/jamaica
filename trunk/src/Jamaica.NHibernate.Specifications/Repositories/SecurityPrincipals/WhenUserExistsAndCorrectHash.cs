using System;
using Jamaica.NHibernate.Repositories;
using Jamaica.Security;
using NUnit.Framework;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.NHibernate.Specifications.Repositories.SecurityPrincipals
{
    public class WhenUserExistsAndCorrectHash : IntegrationSpecification
    {
        User user;
        ISecurityPrincipal securityPrincipal;

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
            securityPrincipal = Subject<SecurityPrincipalRepository>().GetByNameAndHash(user.Name, user.Hash);
        }

        [Then]
        public void UserReturnedAsSecurityPrincipal()
        {
            Verify(securityPrincipal, Is.EqualTo(user));
        }
    }
}