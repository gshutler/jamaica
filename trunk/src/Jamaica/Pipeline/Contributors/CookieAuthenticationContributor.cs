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
            pipelineRunner.Notify(SetUser)
                .After<KnownStages.IHandlerSelection>();
        }

        public PipelineContinuation SetUser(ICommunicationContext context)
        {
            var cookieAuth = resolver.Resolve<ICookieAuthenticationService>();

            var authorizedUser = cookieAuth.AuthorizedUser();
            resolver.AddDependencyInstance<User>(authorizedUser);

            if (authorizedUser != User.Anonymous)
            {
                context.User = CreateSecurityPrincipal(authorizedUser);
                return PipelineContinuation.Continue;
            }

            cookieAuth.ClearCookies();

            return PipelineContinuation.Continue;
        }

        static IPrincipal CreateSecurityPrincipal(User user)
        {
            var identity = new GenericIdentity(user.Username);
            var roles = user.Roles.Select(role => role.Name).ToArray();

            return new GenericPrincipal(identity, roles);
        }
    }
}