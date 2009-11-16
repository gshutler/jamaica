using System;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.Test.Pipeline.Contributors
{
    public class DummyResponseCodingContributor : KnownStages.IResponseCoding
    {
        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(HandlerSelection)
                .After<KnownStages.IBegin>();
        }

        PipelineContinuation HandlerSelection(ICommunicationContext context)
        {
            return PipelineContinuation.Continue;
        }
    }
}