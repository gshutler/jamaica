using System;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.Pipeline.Contributors
{
    public class PersistenceInitializedContributor : KnownContributors.IPersistenceInitialized
    {
        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(Continue)
                .After<KnownStages.IHandlerSelection>();
        }

        PipelineContinuation Continue(ICommunicationContext context)
        {
            return PipelineContinuation.Continue;
        }
    }
}