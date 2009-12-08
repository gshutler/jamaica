using System;
using Jamaica.Security;
using NUnit.Framework;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Security.Users
{
    public class VerifyingCorrectPassword : Specification
    {
        User user;
        bool result;

        protected override void Given()
        {
            user = new User("NathanTyson");

            user.SetPassword("forest");
        }

        protected override void When()
        {
            result = user.VerifyPassword("forest");
        }

        [Then]
        public void ConfirmsPasswordCorrect()
        {
            Verify(result, Is.True);
        }
    }
}