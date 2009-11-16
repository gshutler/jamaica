using System;
using Jamaica.NHibernate.Pipeline.Contributors;
using Jamaica.Pipeline.Contributors;
using Jamaica.Test.Pipeline;
using Jamaica.Test.Pipeline.Contributors;
using NUnit.Framework;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Pipeline.Contributors;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.NHibernate.Specifications.Pipeline.Contributors.SessionInitializationAlwaysBeforeCookieAuthentication
{
    public class RegisteringCookieAuthenticationThenSessionInitialization : Specification
    {
        IPipeline pipeline;

        protected override void Given()
        {
            Dependency<IDependencyResolver>()
                .Stub(x => x.ResolveAll<IPipelineContributor>())
                .Return(new IPipelineContributor[]
                            {
                                new PersistenceInitializedContributor(),
                                new DummyHandlerSelectionContributor(),
                                new CookieAuthenticationContributor(Dependency<IDependencyResolver>()),
                                Subject<SessionInitializationContributor>(),
                                new BootstrapperContributor()
                            });

            pipeline = new PipelineRunner(Dependency<IDependencyResolver>());
        }

        protected override void When()
        {
            pipeline.Initialize();
        }

        [Then]
        public void SessionManagementComesBeforeCookieAuthentication()
        {
            Verify(
                pipeline.IndexOf<SessionInitializationContributor>(), 
                Is.LessThan(pipeline.IndexOf<CookieAuthenticationContributor>()));
        }
    }
}