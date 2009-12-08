using System;
using OpenRasta.Configuration;
using OpenRasta.DI;

namespace Jamaica.Configuration
{
    public class DependencyResolverAccessor : IDependencyResolverAccessor
    {
        public IDependencyResolver Resolver
        {
            get
            {
                var resolver = new InternalDependencyResolver();

                resolver.AddDependency<IDependencyRegistrar, DependencyRegistrar>();

                return resolver;
            }
        }
    }
}