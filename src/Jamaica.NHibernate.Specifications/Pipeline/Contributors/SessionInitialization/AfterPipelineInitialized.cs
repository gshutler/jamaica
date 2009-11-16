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
        IPipeline pipeline;

        protected override void Given()
        {
            Dependency<IDependencyResolver>()
                .Stub(x => x.ResolveAll<IPipelineContributor>())
                .Return(new IPipelineContributor[]
                            {
                                Subject<SessionInitializationContributor>(),
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
        public void BeforePersistenceInitialized()
        {
            Verify(
                pipeline.IndexOf<SessionInitializationContributor>(),
                Is.LessThan(pipeline.IndexOf<KnownContributors.IPersistenceInitialized>()));
        }
    }
}