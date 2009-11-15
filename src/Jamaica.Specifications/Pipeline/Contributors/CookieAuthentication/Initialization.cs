using System;
using System.IO;
using Jamaica.Pipeline.Contributors;
using Jamaica.Services;
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

        protected override void Given()
        {
            Dependency<IDependencyResolver>()
                .Stub(x => x.Resolve<ICookieAuthenticationService>())
                .Return(Dependency<ICookieAuthenticationService>());

            Dependency<IDependencyResolver>()
                .Stub(x => x.ResolveAll<IPipelineContributor>())
                .Return(new IPipelineContributor[]
                            {
                                new DummyHandlerSelectionContributor(),
                                new CookieAuthenticationContributor(Dependency<IDependencyResolver>()),
                                new BootstrapperContributor()
                            });
            
            pipeline = new PipelineRunner(Dependency<IDependencyResolver>());
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