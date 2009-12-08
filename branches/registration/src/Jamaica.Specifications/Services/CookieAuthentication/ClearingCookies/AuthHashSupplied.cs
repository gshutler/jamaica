using System;
using System.Collections.Generic;
using System.Linq;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Services.CookieAuthentication.ClearingCookies
{
    public class AuthHashSupplied : Specification
    {
        ResponseCookieCollection responseCookies;

        protected override void Given()
        {
            var requestCookies = new RequestCookieCollection(
                new KeyValuePair<string, string>("__authHash", "hash"));

            Dependency<IResponse>()
                .Stub(x => x.Cookies)
                .Return(responseCookies = new ResponseCookieCollection());

            Dependency<IRequest>()
                .Stub(x => x.Cookies)
                .Return(requestCookies);
        }

        protected override void When()
        {
            Subject<CookieAuthenticationService>().ClearCookies();
        }

        [Then]
        public void AuthHashRemoved()
        {
            var cookie = responseCookies.Single();

            Verify(cookie.Name, Is.EqualTo("__authHash"));
            Verify(cookie.Discard, Is.True);
        }
    }
}