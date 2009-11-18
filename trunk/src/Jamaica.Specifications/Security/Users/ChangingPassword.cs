using System;
using Jamaica.Security;
using NUnit.Framework;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Security.Users
{
    public class ChangingPassword : Specification
    {
        User user;
        string previousSalt;
        string previousHash;

        protected override void Given()
        {
            user = new User("NathanTyson");

            user.SetPassword("forest");

            previousSalt = user.Salt;
            previousHash = user.Hash;
        }

        protected override void When()
        {
            user.SetPassword("forest");
        }

        [Then]
        public void NewSaltUsed()
        {
            Verify(user.Salt, Is.Not.EqualTo(previousSalt));
        }

        [Then]
        public void NewHashGenerated()
        {
            Verify(user.Hash, Is.Not.EqualTo(previousHash));
        }
    }
}