using System;
using Jamaica.Configuration;
using Jamaica.Pipeline.Contributors;
using NUnit.Framework;
using OpenRasta.Configuration.Fluent;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Configuration
{
    public class ConfiguringCookieAuthentication : Specification
    {
        readonly IDependencyResolver resolver = new InternalDependencyResolver();

        protected override void Given()
        {
            Dependency<IUses>()
                .Stub(x => x.Resolver)
                .Return(resolver);
        }

        protected override void When()
        {
            Dependency<IUses>().CookieAuthentication();
        }

        [Then]
        public void AddsCookieAuthenticationContributorToResolver()
        {
            Verify(
                resolver.HasDependencyImplementation(typeof(IPipelineContributor), typeof(CookieAuthenticationContributor)), 
                Is.True);
        }
    }
}