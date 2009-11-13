using Jamaica.Pipeline.Contributors;
using OpenRasta.Configuration;
using OpenRasta.Configuration.Fluent;

namespace Jamaica.Configuration
{
    public static class UsesExtensions
    {
        public static void CookieAuthentication(this IUses uses)
        {
            uses.PipelineContributor<CookieAuthenticationContributor>();
        }
    }
}