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

namespace Jamaica.Specifications.Pipeline.Contributors.PersistenceInitialized
{
    public class WhenRegistered : Specification
    {
        protected override void Given()
        {
            Resolver.AddDependencyInstance<IPipelineContributor>(new PersistenceInitializedContributor());
            Resolver.AddDependencyInstance<IPipelineContributor>(new DummyHandlerSelectionContributor());
            Resolver.AddDependencyInstance<IPipelineContributor>(new BootstrapperContributor());
        }

        protected override void When()
        {
            Subject<PipelineRunner>().Initialize();
        }
        
        [Then]
        public void AfterHandlerSelection()
        {
            Verify(Subject<PipelineRunner>().IndexOf<KnownContributors.IPersistenceInitialized>(),
                   Is.GreaterThan(Subject<PipelineRunner>().IndexOf<DummyHandlerSelectionContributor>()));
        }
    }
}