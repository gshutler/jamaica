using Jamaica.NHibernate.Pipeline.Contributors;
using Jamaica.NHibernate.Repositories;
using Jamaica.Repositories;
using OpenRasta.Configuration;
using OpenRasta.Configuration.Fluent;
using OpenRasta.DI;

namespace Jamaica.NHibernate
{
    public static class JamaicaNHibernateManagementExtension
    {
        public static void JamaicanNHibernateManagement(this IUses anchor)
        {
            anchor.Resolver.AddDependency<IUserRepository, UserRepository>(DependencyLifetime.Transient);
            anchor.PipelineContributor<SessionManagementContributor>();
        }
    }
}