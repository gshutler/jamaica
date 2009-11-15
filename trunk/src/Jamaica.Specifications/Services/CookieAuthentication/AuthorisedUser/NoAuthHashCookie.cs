using System;
using System.Collections.Generic;
using Jamaica.Security;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Services.CookieAuthentication.AuthorisedUser
{
    public class NoAuthHashCookie : Specification
    {
        User authorizedUser;

        protected override void Given()
        {
            var cookies = new RequestCookieCollection(
                new KeyValuePair<string, string>("__authName", "userName")
                );

            Dependency<IRequest>()
                .Stub(x => x.Cookies)
                .Return(cookies);
        }

        protected override void When()
        {
            authorizedUser = Subject<CookieAuthenticationService>().AuthorizedUser();
        }

        [Then]
        public void AnonymousUser()
        {
            Verify(authorizedUser, Is.SameAs(User.Anonymous));
        }
    }
}