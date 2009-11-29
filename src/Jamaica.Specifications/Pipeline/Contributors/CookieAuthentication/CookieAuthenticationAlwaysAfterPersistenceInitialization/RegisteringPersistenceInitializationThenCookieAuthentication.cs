using System;
using Jamaica.Pipeline.Contributors;
using Jamaica.Test.Pipeline;
using Jamaica.Test.Pipeline.Contributors;
using NUnit.Framework;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Pipeline.Contributors;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Pipeline.Contributors.CookieAuthentication.CookieAuthenticationAlwaysAfterPersistenceInitialization
{
    public class RegisteringPersistenceInitializationThenCookieAuthentication : Specification
    {
        protected override void Given()
        {
            Resolver.AddDependencyInstance<IPipelineContributor>(new PersistenceInitializedContributor());
            Resolver.AddDependencyInstance<IPipelineContributor>(new DummyHandlerSelectionContributor());
            Resolver.AddDependencyInstance<IPipelineContributor>(Subject<CookieAuthenticationContributor>());
            Resolver.AddDependencyInstance<IPipelineContributor>(new BootstrapperContributor());
        }

        protected override void When()
        {
            Subject<PipelineRunner>().Initialize();
        }

        [Then]
        public void CookieAuthenticationAfterPersistenceInitialization()
        {
            Verify(
                Subject<PipelineRunner>().IndexOf<CookieAuthenticationContributor>(), 
                Is.GreaterThan(Subject<PipelineRunner>().IndexOf<KnownContributors.IPersistenceInitialized>()));
        }
    }
}