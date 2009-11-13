using System;
using System.IO;
using Jamaica.Pipeline.Contributors;
using NUnit.Framework;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Pipeline.Contributors;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Pipeline.Contributors.CookieAuthentication
{
    public class Initialization : Specification
    {
        IPipeline pipeline;
        readonly IDependencyResolver resolver = new InternalDependencyResolver();

        protected override void Given()
        {
            resolver.AddDependency<IPipelineContributor, BootstrapperContributor>();
            resolver.AddDependency<IPipelineContributor, DummyHandlerSelectionContributor>();
            resolver.AddDependency<IPipelineContributor, CookieAuthenticationContributor>();

            pipeline = new PipelineRunner(resolver);
        }

        protected override void When()
        {
            pipeline.Initialize();
        }

        [Then]
        public void RunAfterHandlerSelection()
        {
            Verify(IndexOf<CookieAuthenticationContributor>(), 
                Is.GreaterThan(IndexOf<DummyHandlerSelectionContributor>()));
        }
        
        [Then]
        public void RegistersTheSetUserMethod()
        {
            // check OR core to work out how to test the right method is hooked up
            Verify(true, Is.False);
        }

        int IndexOf<T>() where T : IPipelineContributor
        {
            var index = 0;

            foreach (var call in pipeline.CallGraph)
            {
                if (typeof(T).IsAssignableFrom(call.Target.GetType()))
                    return index;
                index++;
            }

            throw new InvalidDataException();
        }

        class DummyHandlerSelectionContributor : KnownStages.IHandlerSelection
        {
            public void Initialize(IPipeline pipelineRunner)
            {
                pipelineRunner.Notify(HandlerSelection)
                    .After<KnownStages.IBegin>();
            }

            PipelineContinuation HandlerSelection(ICommunicationContext arg)
            {
                return PipelineContinuation.Continue;
            }
        }
    }
}