using System;
using Jamaica.Security;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Services.CookieAuthentication.AuthorisedUser
{
    public class NoAuthNameCookie : Specification
    {
        User authorizedUser;

        protected override void Given()
        {
            Dependency<IRequest>()
                .Stub(x => x.Cookies)
                .Return(new RequestCookieCollection());
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