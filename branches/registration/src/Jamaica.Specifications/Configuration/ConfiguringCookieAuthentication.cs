using System;
using Jamaica.Configuration;
using Jamaica.Pipeline.Contributors;
using Jamaica.Services;
using NUnit.Framework;
using OpenRasta.Configuration.Fluent.Implementation;
using OpenRasta.Configuration.MetaModel;
using OpenRasta.Pipeline;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Configuration
{
    public class ConfiguringCookieAuthentication : Specification
    {
        protected override void Given()
        {
            InjectDependency(new MetaModelRepository(Resolver));
        }

        protected override void When()
        {
            Subject<FluentTarget>().CookieAuthentication();
        }

        [Then]
        public void RegistersCookieAuthenticationContributor()
        {
            Verify(
                Resolver.HasDependencyImplementation(typeof(IPipelineContributor), typeof(CookieAuthenticationContributor)), 
                Is.True);
        }

        [Then]
        public void RegistersCookieAuthenticationService()
        {
            Verify(
                Resolver.HasDependencyImplementation(typeof(ICookieAuthenticationService), typeof(CookieAuthenticationService)),
                Is.True);
        }
    }
}