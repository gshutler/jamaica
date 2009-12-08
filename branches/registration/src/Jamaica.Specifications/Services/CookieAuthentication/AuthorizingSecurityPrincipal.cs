using System;
using System.Linq;
using Jamaica.Security;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Services.CookieAuthentication
{
    public class AuthorizingSecurityPrincipal : Specification
    {
        ResponseCookieCollection cookieCollection;
        User user;

        protected override void Given()
        {
            cookieCollection = new ResponseCookieCollection();

            user = new User("NathanTyson");
            user.SetPassword("forest");

            Dependency<IResponse>()
                .Stub(x => x.Cookies)
                .Return(cookieCollection);
        }

        protected override void When()
        {
            Subject<CookieAuthenticationService>().AuthorizeSecurityPrincipal(user);
        }

        [Then]
        public void AuthNameCookieSetToUserName()
        {
            var authNameCookie = cookieCollection.Single(c => c.Name == "__authName");

            Verify(authNameCookie.Value, Is.EqualTo(user.Name));
            Verify(authNameCookie.Path, Is.EqualTo("/"));
        }

        [Then]
        public void AuthHashCookieSetToUserHash()
        {
            var authHashCookie = cookieCollection.Single(c => c.Name == "__authHash");

            Verify(authHashCookie.Value, Is.EqualTo(user.Hash));
            Verify(authHashCookie.Path, Is.EqualTo("/"));
        }
    }
}