using System;
using System.Linq;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Services.CookieAuthentication.ClearingCookies
{
    public class NoCookiesSupplied : Specification
    {
        ResponseCookieCollection responseCookies;

        protected override void Given()
        {
            var requestCookies = new RequestCookieCollection();

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
        public void NothingRemoved()
        {
            Verify(responseCookies, Is.Empty);
        }
    }
}