using System;
using Jamaica.NHibernate.Repositories;
using Jamaica.Security;
using Jamaica.Test;
using NUnit.Framework;
using Rhino.Mocks;

namespace Jamaica.NHibernate.Specifications.Repositories.Users
{
    public class WhenUserDoesNotExist : IntegrationSpecification
    {
        User user;

        protected override void Given()
        {
        }

        protected override void When()
        {
            user = Subject<UserRepository>().GetByUsernameAndHash("not_exist", "does_not_matter");
        }

        [Then]
        public void UserIsNull()
        {
            Verify(user, Is.Null);
        }
    }
}