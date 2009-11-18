using System;
using Jamaica.Security;
using NUnit.Framework;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Security.Users
{
    public class SettingPassword : Specification
    {
        User user;

        protected override void Given()
        {
            user = new User("NathanTyson");
        }

        protected override void When()
        {
            user.SetPassword("forest");
        }

        [Then]
        public void SaltIsSet()
        {
            Verify(string.IsNullOrEmpty(user.Salt), Is.False);
        }

        [Then]
        public void HashIsSet()
        {
            var expectedHash = ("forest" + user.Salt).GenerateHexHashString();

            Verify(user.Hash, Is.EqualTo(expectedHash));
        }
    }
}