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
        IPipeline pipeline;

        protected override void Given()
        {
            Dependency<IDependencyResolver>()
                .Stub(x => x.ResolveAll<IPipelineContributor>())
                .Return(new IPipelineContributor[]
                            {
                                new PersistenceInitializedContributor(),
                                new DummyHandlerSelectionContributor(),
                                new BootstrapperContributor()
                            });

            pipeline = new PipelineRunner(Dependency<IDependencyResolver>());
        }

        protected override void When()
        {
            pipeline.Initialize();
        }
        
        [Then]
        public void AfterHandlerSelection()
        {
            Verify(pipeline.IndexOf<KnownContributors.IPersistenceInitialized>(),
                   Is.GreaterThan(pipeline.IndexOf<DummyHandlerSelectionContributor>()));
        }
    }
}