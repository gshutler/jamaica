using System;
using Jamaica.Security;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Services.CookieAuthentication.AuthorizedSecurityPrincipal
{
    public class NoAuthNameCookie : Specification
    {
        ISecurityPrincipal authorizedUser;

        protected override void Given()
        {
            Dependency<IRequest>()
                .Stub(x => x.Cookies)
                .Return(new RequestCookieCollection());
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