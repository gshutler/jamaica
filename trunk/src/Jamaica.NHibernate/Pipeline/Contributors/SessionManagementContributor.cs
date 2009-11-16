using System;
using Jamaica.Pipeline.Contributors;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.NHibernate.Pipeline.Contributors
{
    public class SessionManagementContributor : IPipelineContributor
    {
        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(BeginSession)
                .Before<KnownContributors.IPersistenceInitialized>();
        }

        PipelineContinuation BeginSession(ICommunicationContext arg)
        {
            throw new NotImplementedException();
        }
    }
}