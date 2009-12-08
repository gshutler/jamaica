using System;
using Jamaica.Services;
using Jamaica.TableFootball.Core.Authentication.Logout;
using Jamaica.TableFootball.Core.Home;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Authentication.Logout
{
    public class LoggingOut : Specification
    {
        OperationResult result;

        protected override void Given()
        {
            UriResolver.Add(new UriRegistration("/home", typeof(HomeResource)));
        }

        protected override void When()
        {
            result = Subject<LogoutHandler>().Get();
        }

        [Then]
        public void RedirectedToHomePage()
        {
            Verify(result, Is.InstanceOf<OperationResult.SeeOther>());
            Verify(result.RedirectLocation, Is.EqualTo(typeof(HomeResource).CreateUri()));
        }

        [Then]
        public void AuthenticationCookiesCleared()
        {
            Dependency<ICookieAuthenticationService>()
                .AssertWasCalled(x => x.ClearCookies());
        }
    }
}