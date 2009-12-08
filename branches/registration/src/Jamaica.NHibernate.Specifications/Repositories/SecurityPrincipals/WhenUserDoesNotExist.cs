using System;
using Jamaica.NHibernate.Repositories;
using Jamaica.Security;
using Jamaica.Test;
using NUnit.Framework;
using Rhino.Mocks;

namespace Jamaica.NHibernate.Specifications.Repositories.SecurityPrincipals
{
    public class WhenUserDoesNotExist : IntegrationSpecification
    {
        ISecurityPrincipal securityPrincipal;

        protected override void Given()
        {
        }

        protected override void When()
        {
            securityPrincipal = Subject<SecurityPrincipalRepository>().GetByNameAndHash("not_exist", "does_not_matter");
        }

        [Then]
        public void SecurityPrincipalIsNull()
        {
            Verify(securityPrincipal, Is.Null);
        }
    }
}