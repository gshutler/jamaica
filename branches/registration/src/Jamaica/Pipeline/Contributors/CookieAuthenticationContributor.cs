using System;
using System.Linq;
using System.Security.Principal;
using Jamaica.Security;
using Jamaica.Services;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.Pipeline.Contributors
{
    public class CookieAuthenticationContributor : IPipelineContributor
    {
        readonly IDependencyResolver resolver;

        public CookieAuthenticationContributor(IDependencyResolver resolver)
        {
            this.resolver = resolver;
        }

        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(SetSecurityPrincipal)
                .After<KnownContributors.IPersistenceInitialized>();
        }

        public PipelineContinuation SetSecurityPrincipal(ICommunicationContext context)
        {
            var cookieAuth = resolver.Resolve<ICookieAuthenticationService>();

            var authorizedUser = cookieAuth.AuthorizedSecurityPrincipal();
            resolver.AddDependencyInstance<ISecurityPrincipal>(authorizedUser);

            if (authorizedUser != User.Anonymous)
            {
                context.User = CreateSecurityPrincipal(authorizedUser);
                return PipelineContinuation.Continue;
            }

            cookieAuth.ClearCookies();

            return PipelineContinuation.Continue;
        }

        static IPrincipal CreateSecurityPrincipal(ISecurityPrincipal securityPrincipal)
        {
            var identity = new GenericIdentity(securityPrincipal.Name);
            var roles = securityPrincipal.Roles.Select(role => role.Name).ToArray();

            return new GenericPrincipal(identity, roles);
        }
    }
}