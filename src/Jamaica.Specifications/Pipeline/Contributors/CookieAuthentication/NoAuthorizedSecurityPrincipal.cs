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
    public class NoAuthorizedSecurityPrincipal : Specification
    {
        PipelineContinuation continuation;

        protected override void Given()
        {
            AddDependencyToResolver<ICookieAuthenticationService>();

            Dependency<ICookieAuthenticationService>()
                .Stub(x => x.AuthorizedSecurityPrincipal())
                .Return(User.Anonymous);
        }

        protected override void When()
        {
            continuation = Subject<CookieAuthenticationContributor>().SetSecurityPrincipal(Dependency<ICommunicationContext>());
        }

        [Then]
        public void PipelineContinues()
        {
            Verify(continuation, Is.EqualTo(PipelineContinuation.Continue));
        }

        [Then]
        public void AnonymousUserIsRegisteredAsSecurityPrincipal()
        {
            Verify(Resolver.Resolve<ISecurityPrincipal>(), Is.SameAs(User.Anonymous));
        }

        [Then]
        public void ContextUserIsNotSet()
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