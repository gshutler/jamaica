using System;
using Jamaica.NHibernate.Pipeline.Contributors;
using NHibernate;
using NUnit.Framework;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.NHibernate.Specifications.Pipeline.Contributors.SessionResolution
{
    public class ResolvingSessionWhenTransactionCommitted : Specification
    {
        PipelineContinuation result;

        protected override void Given()
        {
            AddDependencyToResolver<ISession>();

            Dependency<ISession>()
                .Stub(x => x.Transaction)
                .Return(Dependency<ITransaction>());

            Dependency<ITransaction>()
                .Stub(x => x.IsActive)
                .Return(false);
        }

        protected override void When()
        {
            result = Subject<SessionResolutionContributor>().ResolveSession(Dependency<ICommunicationContext>());
        }

        [Then]
        public void PipelineContinues()
        {
            Verify(result, Is.EqualTo(PipelineContinuation.Continue));
        }

        [Then]
        public void TransactionDisposed()
        {
            Dependency<ITransaction>()
                .AssertWasCalled(x => x.Dispose());
        }

        [Then]
        public void SessionDisposed()
        {
            Dependency<ISession>()
                .AssertWasCalled(x => x.Dispose());
        }
    }
}