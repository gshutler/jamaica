using System;
using Jamaica.Pipeline.Contributors;
using NHibernate;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.NHibernate.Pipeline.Contributors
{
    public class SessionInitializationContributor : IPipelineContributor
    {
        readonly IDependencyResolver resolver;
        readonly ISessionFactory sessionFactory;

        public SessionInitializationContributor(IDependencyResolver resolver, ISessionFactory sessionFactory)
        {
            this.resolver = resolver;
            this.sessionFactory = sessionFactory;
        }

        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(InitializeSession)
                .Before<KnownContributors.IPersistenceInitialized>();
        }

        public PipelineContinuation InitializeSession(ICommunicationContext arg)
        {
            var session = sessionFactory.OpenSession();

            resolver.AddDependencyInstance<ISession>(session, DependencyLifetime.PerRequest);
            session.BeginTransaction();

            return PipelineContinuation.Continue;
        }
    }
}