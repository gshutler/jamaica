using System;
using NUnit.Framework;
using OpenRasta.Configuration;
using OpenRasta.DI;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.Specifications.Configuration
{
    public class GeneratedResolver : Specification
    {
        private IDependencyResolver resolver;

        protected override void Given()
        {
        }

        protected override void When()
        {
            resolver = Subject<Jamaica.Configuration.DependencyResolverAccessor>().Resolver;
        }

        [Then]
        public void InstanceOfInternalDependencyResolver()
        {
            Verify(resolver, Is.TypeOf<InternalDependencyResolver>());
        }

        [Then]
        public void RegisteredDependencyRegistrar()
        {
            Verify(
                resolver.HasDependencyImplementation(typeof(IDependencyRegistrar), typeof(Jamaica.Configuration.DependencyRegistrar)),
                Is.True);
        }
    }
}