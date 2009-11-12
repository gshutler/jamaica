using System;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.Pipeline.Contributors
{
    public class PersistenceHandleContributor : IPipelineContributor
    {
        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(Handle)
                .After<KnownStages.IHandlerSelection>();
        }

        private PipelineContinuation Handle(ICommunicationContext context)
        {
            return PipelineContinuation.Continue;
        }
    }
}