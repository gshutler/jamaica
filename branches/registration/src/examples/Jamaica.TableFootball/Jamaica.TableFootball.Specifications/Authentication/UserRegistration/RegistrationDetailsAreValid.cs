using System;
using Jamaica.Security;
using Jamaica.Services;
using Jamaica.TableFootball.Core.Authentication.UserRegistration;
using Jamaica.TableFootball.Core.Home;
using NHibernate;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Authentication.UserRegistration
{
    public class RegistrationDetailsAreValid : Specification
    {
        UserRegistrationResource userRegistrationResource;
        OperationResult result;

        protected override void Given()
        {
            userRegistrationResource = new UserRegistrationResource
                                           {
                                               Name = "NathanTyson",
                                               Password = "Forest",
                                               PasswordConfirmation = "Forest"
                                           };

            UriResolver.Add(new UriRegistration("/home", typeof (HomeResource)));
        }

        protected override void When()
        {
            result = Subject<UserRegistrationHandler>().Post(userRegistrationResource);
        }

        [Then]
        public void RedirectedToHomeResource()
        {
            Verify(result, Is.TypeOf<OperationResult.SeeOther>());
            Verify(result.RedirectLocation, Is.EqualTo(typeof (HomeResource).CreateUri()));
        }

        [Then]
        public void UserSaved()
        {
            Dependency<ISession>()
                .AssertWasCalled(
                x => x.Save(UserCreatedFromRegistrationResource()));
        }

        [Then]
        public void AuthenticationCookiesSet()
        {
            Dependency<ICookieAuthenticationService>()
                .AssertWasCalled(
                x => x.AuthorizeSecurityPrincipal(UserCreatedFromRegistrationResource()));
        }

        static ISecurityPrincipal UserCreatedFromRegistrationResource()
        {
            return Arg<User>
                .Matches(user =>
                         user.Name == "NathanTyson"
                         && user.VerifyPassword("Forest"));
        }
    }
}