using System;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.Test.Pipeline.Contributors
{
    public class DummyCodecResponseSelectionContributor : KnownStages.ICodecResponseSelection
    {
        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(CodecResponseSelection)
                .After<KnownStages.IHandlerSelection>();
        }

        PipelineContinuation CodecResponseSelection(ICommunicationContext arg)
        {
            return PipelineContinuation.Continue;
        }
    }
}