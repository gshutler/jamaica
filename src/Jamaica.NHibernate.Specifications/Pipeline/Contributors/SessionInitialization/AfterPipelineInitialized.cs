using System;
using Jamaica.NHibernate.Pipeline.Contributors;
using Jamaica.Pipeline.Contributors;
using Jamaica.Test.Pipeline;
using Jamaica.Test.Pipeline.Contributors;
using NUnit.Framework;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Pipeline.Contributors;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.NHibernate.Specifications.Pipeline.Contributors.SessionInitialization
{
    public class AfterPipelineInitialized : Specification
    {
        protected override void Given()
        {
            Resolver.AddDependencyInstance<IPipelineContributor>(Subject<SessionInitializationContributor>());
            Resolver.AddDependencyInstance<IPipelineContributor>(new PersistenceInitializedContributor());
            Resolver.AddDependencyInstance<IPipelineContributor>(new DummyHandlerSelectionContributor());
            Resolver.AddDependencyInstance<IPipelineContributor>(new BootstrapperContributor());
        }

        protected override void When()
        {
            Subject<PipelineRunner>().Initialize();
        }

        [Then]
        public void BeforePersistenceInitialized()
        {
            Verify(
                Subject<PipelineRunner>().IndexOf<SessionInitializationContributor>(),
                Is.LessThan(Subject<PipelineRunner>().IndexOf<KnownContributors.IPersistenceInitialized>()));
        }
    }
}