using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.Test.Pipeline.Contributors
{
    public class DummyHandlerSelectionContributor : KnownStages.IHandlerSelection
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