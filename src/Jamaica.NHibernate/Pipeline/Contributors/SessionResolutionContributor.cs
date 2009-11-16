using System;
using NHibernate;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.NHibernate.Pipeline.Contributors
{
    public class SessionResolutionContributor : IPipelineContributor
    {
        readonly IDependencyResolver resolver;

        public SessionResolutionContributor(IDependencyResolver resolver)
        {
            this.resolver = resolver;
        }

        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(ResolveSession)
                .Before<KnownStages.IResponseCoding>();
        }

        public PipelineContinuation ResolveSession(ICommunicationContext context)
        {
            var session = resolver.Resolve<ISession>();
            var transaction = session.Transaction;

            if (transaction.IsActive)
            {
                transaction.Commit();    
            }

            transaction.Dispose();
            session.Dispose();

            return PipelineContinuation.Continue;
        }
    }
}