using System;
using Jamaica.NHibernate.Configuration;
using Jamaica.NHibernate.Pipeline.Contributors;
using Jamaica.NHibernate.Repositories;
using Jamaica.Repositories;
using NUnit.Framework;
using OpenRasta.Configuration.Fluent.Implementation;
using OpenRasta.Configuration.MetaModel;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.NHibernate.Specifications.Configuration
{
    public class ConfiguringNHibernatePersistence : Specification
    {
        IDependencyResolver resolver;
        IMetaModelRepository metaModel;

        protected override void Given()
        {
            resolver = new InternalDependencyResolver();
            metaModel = new MetaModelRepository(resolver);
            Inject(resolver);
            Inject(metaModel);
        }

        protected override void When()
        {
            Subject<FluentTarget>().NHibernatePersistence();
        }

        [Then]
        public void UserRepositoryImplementationRegistered()
        {
            Verify(
                resolver.HasDependencyImplementation(typeof(IUserRepository), typeof(UserRepository)), 
                Is.True);
        }

        [Then]
        public void SessionManagementContributorRegistered()
        {
            Verify(
                resolver.HasDependencyImplementation(typeof(IPipelineContributor), typeof(SessionManagementContributor)), 
                Is.True);
        }
    }
}