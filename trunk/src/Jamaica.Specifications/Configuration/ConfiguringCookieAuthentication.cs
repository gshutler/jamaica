using System;
using Jamaica.Configuration;
using Jamaica.Pipeline.Contributors;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.Configuration.Fluent;
using OpenRasta.Configuration.Fluent.Implementation;
using OpenRasta.Configuration.MetaModel;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Configuration
{
    public class ConfiguringCookieAuthentication : Specification
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
            Subject<FluentTarget>().CookieAuthentication();
        }

        [Then]
        public void RegistersCookieAuthenticationContributor()
        {
            Verify(
                resolver.HasDependencyImplementation(typeof(IPipelineContributor), typeof(CookieAuthenticationContributor)), 
                Is.True);
        }

        [Then]
        public void RegistersCookieAuthenticationService()
        {
            Verify(
                resolver.HasDependencyImplementation(typeof(ICookieAuthenticationService), typeof(CookieAuthenticationService)),
                Is.True);
        }
    }
}