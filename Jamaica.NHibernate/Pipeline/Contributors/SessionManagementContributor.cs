using System;
using Jamaica.Pipeline.Contributors;
using NHibernate;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.NHibernate.Pipeline.Contributors
{
    public class SessionManagementContributor : IPipelineContributor
    {
        private readonly ISessionFactory sessionFactory;
        private readonly IDependencyResolver resolver;

        public SessionManagementContributor(ISessionFactory sessionFactory, IDependencyResolver resolver)
        {
            this.sessionFactory = sessionFactory;
            this.resolver = resolver;
        }

        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(OpenSession)
                .Before<PersistenceHandleContributor>();

            pipelineRunner.Notify(CloseSession)
                .Before<KnownStages.ICodecResponseSelection>();
        }

        private PipelineContinuation OpenSession(ICommunicationContext context)
        {
            var session = sessionFactory.OpenSession();
            session.BeginTransaction();

            resolver.AddDependencyInstance<ISession>(session, DependencyLifetime.PerRequest);

            return PipelineContinuation.Continue;
        }

        private PipelineContinuation CloseSession(ICommunicationContext context)
        {
            if (!resolver.HasDependency<ISession>())
                return PipelineContinuation.Continue;

            var session = resolver.Resolve<ISession>();

            if (session.Transaction.IsActive)
            {
                session.Transaction.Rollback();
            }

            session.Transaction.Dispose();
            session.Dispose();

            return PipelineContinuation.Continue;
        }
    }
}