using System;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.Pipeline.Contributors
{
    public class CookieAuthenticationContributor : IPipelineContributor
    {
        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(SetUser)
                .After<KnownStages.IHandlerSelection>();
        }

        PipelineContinuation SetUser(ICommunicationContext arg)
        {
            throw new NotImplementedException();
        }
    }
}