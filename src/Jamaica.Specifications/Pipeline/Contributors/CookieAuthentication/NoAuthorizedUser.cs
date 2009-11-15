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
    public class NoAuthorizedUser : Specification
    {
        PipelineContinuation continuation;

        protected override void Given()
        {
            Dependency<IDependencyResolver>()
                .Stub(x => x.Resolve<ICookieAuthenticationService>())
                .Return(Dependency<ICookieAuthenticationService>());

            Dependency<ICookieAuthenticationService>()
                .Stub(x => x.AuthorizedUser())
                .Return(User.Anonymous);
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
        public void AnonymousUserIsRegistered()
        {
            Dependency<IDependencyResolver>()
                .AssertWasCalled(x => x.AddDependencyInstance<User>(User.Anonymous));
        }

        [Then]
        public void AuthorizedUserIsNotSet()
        {
            Verify(Dependency<ICommunicationContext>().User, Is.Null);
        }

        [Then]
        public void AuthenticationCookiesAreCleared()
        {
            Dependency<ICookieAuthenticationService>().AssertWasCalled(x => x.ClearCookies());
        }
    }
}