using System;
using Jamaica.Security;
using Jamaica.Services;
using Jamaica.TableFootball.Core.Authentication.Login;
using Jamaica.TableFootball.Core.Home;
using NHibernate;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Authentication.Login
{
    public class CorrectCredentials : Specification
    {
        LoginResource loginResource;
        OperationResult result;
        User nathanTyson;

        protected override void Given()
        {
            loginResource = new LoginResource
            {
                Name = "NathanTyson",
                Password = "Forest"
            };

            nathanTyson = new User("NathanTyson");
            nathanTyson.SetPassword("Forest");

            Dependency<ISession>()
                .Stub(x => x.Get<User>("NathanTyson"))
                .Return(nathanTyson);

            UriResolver.Add(new UriRegistration("/home", typeof(HomeResource)));
        }

        protected override void When()
        {
            result = Subject<LoginHandler>().Post(loginResource);
        }

        [Then]
        public void RedirectedToTheHomePage()
        {
            Verify(result, Is.InstanceOf<OperationResult.SeeOther>());
            Verify(result.RedirectLocation, Is.EqualTo(typeof(HomeResource).CreateUri()));
        }

        [Then]
        public void AuthenticationCookiesSet()
        {
            Dependency<ICookieAuthenticationService>()
                .AssertWasCalled(x => x.AuthorizeSecurityPrincipal(nathanTyson));
        }
    }
}