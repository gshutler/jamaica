using System;
using Jamaica.NHibernate.Repositories;
using Jamaica.Security;
using NUnit.Framework;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.NHibernate.Specifications.Repositories.Users
{
    public class WhenUserExistsButHashWrong : IntegrationSpecification
    {
        User user;
        User newUser;

        protected override void Given()
        {
            using (var transaction = session.BeginTransaction())
            {
                newUser = new User("NathanTyson");

                newUser.SetPassword("forest");

                session.Save(newUser);
                transaction.Commit();
            }

            session.Clear();
        }

        protected override void When()
        {
            user = Subject<UserRepository>().GetByUsernameAndHash(newUser.Username, "wrong_hash");
        }

        [Then]
        public void UserIsNull()
        {
            Verify(user, Is.Null);
        }
    }
}