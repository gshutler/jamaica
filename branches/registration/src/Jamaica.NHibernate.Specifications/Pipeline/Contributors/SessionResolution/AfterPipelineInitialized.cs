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

namespace Jamaica.NHibernate.Specifications.Pipeline.Contributors.SessionResolution
{
    public class AfterPipelineInitialized : Specification
    {
        protected override void Given()
        {
            Resolver.AddDependencyInstance<IPipelineContributor>(new DummyHandlerSelectionContributor());
            Resolver.AddDependencyInstance<IPipelineContributor>(new DummyResponseCodingContributor());
            Resolver.AddDependencyInstance<IPipelineContributor>(new DummyCodecResponseSelectionContributor());
            Resolver.AddDependencyInstance<IPipelineContributor>(Subject<SessionResolutionContributor>());
            Resolver.AddDependencyInstance<IPipelineContributor>(new BootstrapperContributor());
        }

        protected override void When()
        {
            Subject<PipelineRunner>().Initialize();
        }

        [Then]
        public void BeforeResponseCoding()
        {
            Verify(
                Subject<PipelineRunner>().IndexOf<SessionResolutionContributor>(),
                Is.LessThan(Subject<PipelineRunner>().IndexOf<KnownStages.IResponseCoding>()));
        }

        [Then]
        public void AfterCodecResponseSelection()
        {
            Verify(
                Subject<PipelineRunner>().IndexOf<SessionResolutionContributor>(),
                Is.GreaterThan(Subject<PipelineRunner>().IndexOf<KnownStages.ICodecResponseSelection>()));
        }
    }
}