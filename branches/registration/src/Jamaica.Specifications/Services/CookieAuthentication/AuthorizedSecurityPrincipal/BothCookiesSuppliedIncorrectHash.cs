using System;
using System.Collections.Generic;
using Jamaica.Repositories;
using Jamaica.Security;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Services.CookieAuthentication.AuthorizedSecurityPrincipal
{
    public class BothCookiesSuppliedIncorrectHash : Specification
    {
        ISecurityPrincipal authorizedUser;

        protected override void Given()
        {
            var cookies = new RequestCookieCollection(
                new KeyValuePair<string, string>("__authName", "userName"),
                new KeyValuePair<string, string>("__authHash", "bad_hash")
                );

            Dependency<IRequest>()
                .Stub(x => x.Cookies)
                .Return(cookies);

            Dependency<ISecurityPrincipalRepository>()
                .Stub(x => x.GetByNameAndHash("userName", "bad_hash"))
                .Return(null);
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