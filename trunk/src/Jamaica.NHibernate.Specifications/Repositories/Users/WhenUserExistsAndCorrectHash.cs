using System;
using Jamaica.NHibernate.Repositories;
using Jamaica.Security;
using NUnit.Framework;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.NHibernate.Specifications.Repositories.Users
{
    public class WhenUserExistsAndCorrectHash : IntegrationSpecification
    {
        User newUser;
        User user;

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
            user = Subject<UserRepository>().GetByUsernameAndHash(newUser.Username, newUser.Hash);
        }

        [Then]
        public void UserReturned()
        {
            Verify(user, Is.EqualTo(newUser));
        }
    }
}