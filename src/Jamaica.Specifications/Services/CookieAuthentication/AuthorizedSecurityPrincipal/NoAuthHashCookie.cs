using System;
using System.Collections.Generic;
using Jamaica.Security;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Services.CookieAuthentication.AuthorizedSecurityPrincipal
{
    public class NoAuthHashCookie : Specification
    {
        ISecurityPrincipal authorizedUser;

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
            authorizedUser = Subject<CookieAuthenticationService>().AuthorizedSecurityPrincipal();
        }

        [Then]
        public void SecurityPrincipalIsAnonymousUser()
        {
            Verify(authorizedUser, Is.SameAs(User.Anonymous));
        }
    }
}