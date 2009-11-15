using Jamaica.Pipeline.Contributors;
using Jamaica.Services;
using OpenRasta.Configuration;
using OpenRasta.Configuration.Fluent;
using OpenRasta.DI;

namespace Jamaica.Configuration
{
    public static class UsesExtensions
    {
        public static void CookieAuthentication(this IUses uses)
        {
            uses.PipelineContributor<CookieAuthenticationContributor>();
            uses.Resolver.AddDependency<ICookieAuthenticationService, CookieAuthenticationService>(DependencyLifetime.Transient);
        }
    }
}