using System;
using Jamaica;
using OpenRasta.Configuration;
using OpenRasta.DI;

namespace OR.Throwaway
{
    public class DependencyResolverAccesor : IDependencyResolverAccessor
    {
        public IDependencyResolver Resolver
        {
            get
            {
                var resolver = new InternalDependencyResolver();

                resolver.AddDependency(
                    typeof(IDependencyRegistrar), 
                    typeof(DependencyRegistrar), 
                    DependencyLifetime.Singleton);

                return resolver;
            }
        }
    }
}