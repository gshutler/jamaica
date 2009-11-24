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
    public class BothCookiesSuppliedCorrectHash : Specification
    {
        readonly User user = new User("userName");
        ISecurityPrincipal authorizedUser;

        protected override void Given()
        {
            var cookies = new RequestCookieCollection(
                new KeyValuePair<string, string>("__authName", "userName"),
                new KeyValuePair<string, string>("__authHash", "good_hash")
                );

            Dependency<IRequest>()
                .Stub(x => x.Cookies)
                .Return(cookies);

            Dependency<ISecurityPrincipalRepository>()
                .Stub(x => x.GetByNameAndHash("userName", "good_hash"))
                .Return(user);
        }

        protected override void When()
        {
            authorizedUser = Subject<CookieAuthenticationService>().AuthorizedSecurityPrincipal();
        }

        [Then]
        public void SecurityPrincipalIsAuthorizedUser()
        {
            Verify(authorizedUser, Is.SameAs(user));
        }
    }
}