using Jamaica.Repositories;
using Jamaica.Security;
using OpenRasta.DI;
using OpenRasta.Diagnostics;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace Jamaica.Pipeline.Contributors
{
    public class CookieAuthenticationContributor : IPipelineContributor
    {
        private readonly IDependencyResolver resolver;

        public CookieAuthenticationContributor(IDependencyResolver resolver)
        {
            this.resolver = resolver;
        }

        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(ReadCredentials)
                .After<PersistenceHandleContributor>();

            pipelineRunner.Notify(RenderNowWhenUnauthorized)
                .After<KnownStages.IOperationResultInvocation>()
                .And.Before<KnownStages.IResponseCoding>();
        }

        public PipelineContinuation ReadCredentials(ICommunicationContext context)
        {
            var logger = resolver.Resolve<ILogger>();
            var cookies = context.Request.Cookies;

            if (!cookies.Exists(CookieAuthentication.AuthName))
            {
                logger.WriteInfo("AuthName cookie not found");
                return ClearCookiesAndSetUserAsAnonymous(context);
            }

            if (!cookies.Exists(CookieAuthentication.AuthHash))
            {
                logger.WriteInfo("AuthHash cookie not found");
                return ClearCookiesAndSetUserAsAnonymous(context);
            }
                
            var userRepository = resolver.Resolve<IUserRepository>();
            var authUsername = cookies.Get(CookieAuthentication.AuthName);

            logger.WriteInfo("Retrieving user {0}", authUsername);
            var user = userRepository.GetByUsername(authUsername);
                
            if (user == null) 
            {
                logger.WriteInfo("User does not exist");
                return ClearCookiesAndSetUserAsAnonymous(context);
            }

            if (!user.CorrectCookieHash(cookies.Get(CookieAuthentication.AuthHash)))
            {
                logger.WriteInfo("Incorrect AuthHash");
                return ClearCookiesAndSetUserAsAnonymous(context);
            }

            logger.WriteInfo("Setting user as {0}", user.Username);
            resolver.AddDependencyInstance<User>(user, DependencyLifetime.PerRequest);
            context.User = user.CreatePrincipal();

            return PipelineContinuation.Continue;
        }

        private PipelineContinuation ClearCookiesAndSetUserAsAnonymous(ICommunicationContext context)
        {
            RemoveCookieIfItExists(context, CookieAuthentication.AuthHash);
            RemoveCookieIfItExists(context, CookieAuthentication.AuthName);

            resolver.AddDependencyInstance<User>(User.Anonymous, DependencyLifetime.PerRequest);

            return PipelineContinuation.Continue;
        }

        private static void RemoveCookieIfItExists(ICommunicationContext context, string cookieName)
        {
            if (context.Request.Cookies.Exists(cookieName))
                context.Response.Cookies.Remove(cookieName);
        }

        private static PipelineContinuation RenderNowWhenUnauthorized(ICommunicationContext context)
        {
            if (context.OperationResult is OperationResult.Unauthorized)
            {
                return PipelineContinuation.RenderNow;
            }
            return PipelineContinuation.Continue;
        }
    }
}