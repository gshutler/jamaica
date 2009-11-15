using System;
using Jamaica.Pipeline.Contributors;
using Jamaica.Security;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Pipeline.Contributors.CookieAuthentication
{
    public class AuthorizedUser : Specification
    {
        PipelineContinuation continuation;
        User authorizedUser;

        protected override void Given()
        {
            Dependency<IDependencyResolver>()
                .Stub(x => x.Resolve<ICookieAuthenticationService>())
                .Return(Dependency<ICookieAuthenticationService>());

            authorizedUser = new User("Authenticated");

            authorizedUser.AddRole(new Role("Administrator"));
            authorizedUser.AddRole(new Role("Moderator"));

            Dependency<ICookieAuthenticationService>()
                .Stub(x => x.AuthorizedUser())
                .Return(authorizedUser);
        }

        protected override void When()
        {
            continuation = Subject<CookieAuthenticationContributor>().SetUser(Dependency<ICommunicationContext>());
        }

        [Then]
        public void PipelineContinues()
        {
            Verify(continuation, Is.EqualTo(PipelineContinuation.Continue));
        }

        [Then]
        public void AuthorizedUserIsRegistered()
        {
            Dependency<IDependencyResolver>()
                .AssertWasCalled(x => x.AddDependencyInstance<User>(authorizedUser));
        }

        [Then]
        public void UserIdentityIsSet()
        {
            Verify(Dependency<ICommunicationContext>().User.Identity.Name, Is.EqualTo(authorizedUser.Username));
        }

        [Then]
        public void UserRolesAreSet()
        {
            Verify(Dependency<ICommunicationContext>().User.IsInRole("Moderator"), Is.True);
            Verify(Dependency<ICommunicationContext>().User.IsInRole("Administrator"), Is.True);
        }

        [Then]
        public void AuthenticationCookiesAreNotCleared()
        {
            Dependency<ICookieAuthenticationService>().AssertWasNotCalled(x => x.ClearCookies());
        }
    }
}