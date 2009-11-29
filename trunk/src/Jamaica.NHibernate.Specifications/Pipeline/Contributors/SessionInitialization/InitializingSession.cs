using System;
using Jamaica.NHibernate.Pipeline.Contributors;
using NHibernate;
using NUnit.Framework;
using OpenRasta.DI;
using OpenRasta.DI.Internal;
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
            AddDependencyToResolver<ISessionFactory>();
            
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
            var registration = Resolver.Registrations.GetRegistrationForService(typeof(ISession));

            Verify(registration.IsInstanceRegistration, Is.True);
            Verify(registration.LifetimeManager, Is.InstanceOf(typeof(PerRequestLifetimeManager)));
            Verify(Resolver.Resolve<ISession>(), Is.SameAs(Dependency<ISession>()));
        }

        [Then]
        public void TransactionBegun()
        {
            Dependency<ISession>()
                .AssertWasCalled(x => x.BeginTransaction());
        }
    }
}