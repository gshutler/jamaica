using System;
using Jamaica.NHibernate.Pipeline.Contributors;
using NHibernate;
using NUnit.Framework;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.NHibernate.Specifications.Pipeline.Contributors.SessionInitialization
{
    public class InitializingSession : Specification
    {
        PipelineContinuation result;

        protected override void Given()
        {
            Dependency<IDependencyResolver>()
                .Stub(x => x.Resolve<ISessionFactory>())
                .Return(Dependency<ISessionFactory>());

            Dependency<ISessionFactory>()
                .Stub(x => x.OpenSession())
                .Return(Dependency<ISession>());
        }

        protected override void When()
        {
            result = Subject<SessionInitializationContributor>().InitializeSession(Dependency<ICommunicationContext>());
        }

        [Then]
        public void PipelineContinues()
        {
            Verify(result, Is.EqualTo(PipelineContinuation.Continue));
        }

        [Then]
        public void SessionRegistered()
        {
            Dependency<IDependencyResolver>()
                .AssertWasCalled(x => x.AddDependencyInstance<ISession>(Dependency<ISession>(), DependencyLifetime.PerRequest));
        }

        [Then]
        public void TransactionBegun()
        {
            Dependency<ISession>()
                .AssertWasCalled(x => x.BeginTransaction());
        }
    }
}