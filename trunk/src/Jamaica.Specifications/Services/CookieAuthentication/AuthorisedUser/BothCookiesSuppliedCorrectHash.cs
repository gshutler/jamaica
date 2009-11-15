using System;
using System.Collections.Generic;
using Jamaica.Repositories;
using Jamaica.Security;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Services.CookieAuthentication.AuthorisedUser
{
    public class BothCookiesSuppliedCorrectHash : Specification
    {
        readonly User user = new User("userName");
        User authorizedUser;

        protected override void Given()
        {
            var cookies = new RequestCookieCollection(
                new KeyValuePair<string, string>("__authName", "userName"),
                new KeyValuePair<string, string>("__authHash", "good_hash")
                );

            Dependency<IRequest>()
                .Stub(x => x.Cookies)
                .Return(cookies);

            Dependency<IUserRepository>()
                .Stub(x => x.GetByUsernameAndHash("userName", "good_hash"))
                .Return(user);
        }

        protected override void When()
        {
            authorizedUser = Subject<CookieAuthenticationService>().AuthorizedUser();
        }

        [Then]
        public void UserIsAuthorizedUser()
        {
            Verify(authorizedUser, Is.SameAs(user));
        }
    }
}