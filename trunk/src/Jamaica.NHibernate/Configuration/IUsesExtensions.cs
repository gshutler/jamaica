using Jamaica.NHibernate.Pipeline.Contributors;
using Jamaica.NHibernate.Repositories;
using Jamaica.Repositories;
using OpenRasta.Configuration;
using OpenRasta.Configuration.Fluent;
using OpenRasta.DI;

namespace Jamaica.NHibernate.Configuration
{
    public static class IUsesExtensions
    {
        public static void NHibernatePersistence(this IUses uses)
        {
            uses.PipelineContributor<SessionInitializationContributor>();
            uses.PipelineContributor<SessionResolutionContributor>();
            uses.Resolver.AddDependency(typeof(IUserRepository), typeof(UserRepository), DependencyLifetime.Transient);
        }
    }
}