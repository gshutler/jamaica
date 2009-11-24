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

        protected override void Given()
        {
            InjectDependency(resolver = new InternalDependencyResolver());
            InjectDependency(new MetaModelRepository(resolver));
        }

        protected override void When()
        {
            Subject<FluentTarget>().NHibernatePersistence();
        }

        [Then]
        public void SecurityPrincipalRepositoryImplementationRegistered()
        {
            Verify(
                resolver.HasDependencyImplementation(typeof(ISecurityPrincipalRepository), typeof(SecurityPrincipalRepository)), 
                Is.True);
        }

        [Then]
        public void SessionInitializationContributorRegistered()
        {
            Verify(
                resolver.HasDependencyImplementation(typeof(IPipelineContributor), typeof(SessionInitializationContributor)), 
                Is.True);
        }

        [Then]
        public void SessionResolutionContributorRegistered()
        {
            Verify(
                resolver.HasDependencyImplementation(typeof(IPipelineContributor), typeof(SessionResolutionContributor)),
                Is.True);
        }
    }
}