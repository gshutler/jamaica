using System;
using NUnit.Framework;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Pipeline.Contributors;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Configuration
{
    public class RegisteringDependencies : Specification
    {
        readonly IDependencyResolver resolver = new InternalDependencyResolver();

        protected override void Given()
        {
        }

        protected override void When()
        {
            Subject<Jamaica.Configuration.DependencyRegistrar>().Register(resolver);
        }

        [Then]
        public void DigestAuthorizationContributorNotRegistered()
        {
            Verify(
                resolver.HasDependencyImplementation(typeof(IPipelineContributor), typeof(DigestAuthorizerContributor)),
                Is.False);
        }
    }
}