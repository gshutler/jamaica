using Jamaica.Handlers;
using Jamaica.Helpers;
using Jamaica.Security;
using OpenRasta.Configuration;
using OpenRasta.Web;
using OR.Throwaway.Resources;

namespace OR.Throwaway.Handlers
{
    public class LogoutHandler : Handler
    {
        public static void Registration()
        {
            ResourceSpace.Has.ResourcesOfType<LogoutResource>()
                .AtUri("/logout")
                .HandledBy<LogoutHandler>();
        }

        private readonly IResponse response;

        public LogoutHandler(IResponse response)
        {
            this.response = response;
        }

        public OperationResult Get()
        {
            response.RemoveAuthenticationCookies();

            return SeeOther(CreateUri.For<HomeResource>());
        }
    }
}