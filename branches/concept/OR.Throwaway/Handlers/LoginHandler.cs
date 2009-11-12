using Jamaica.Handlers;
using Jamaica.Helpers;
using Jamaica.Repositories;
using Jamaica.Security;
using OpenRasta.Configuration;
using OpenRasta.Web;
using OR.Throwaway.Resources;

namespace OR.Throwaway.Handlers
{
    public class LoginHandler : Handler
    {
        public static void Registration()
        {
            ResourceSpace.Has.ResourcesOfType<LoginResource>()
                .AtUri("/login")
                .HandledBy<LoginHandler>()
                .RenderedByAspx("~/Views/Login.aspx");
        }

        private readonly IUserRepository userRepository;
        private readonly IResponse response;

        public LoginHandler(IUserRepository userRepository, IResponse response)
        {
            this.userRepository = userRepository;
            this.response = response;
        }

        public OperationResult Get()
        {
            return OK(new LoginResource());
        }

        public OperationResult Post(LoginResource loginResource)
        {
            var user = userRepository.GetByUsername(loginResource.Username);

            if (user == null)
            {
                return BadRequest(loginResource, "Unknown username");
            }

            if (!user.CorrectPassword(loginResource.Password))
            {
                return BadRequest(loginResource, "Incorrect password");
            }

            response.SetAuthenticationCookies(user);

            return SeeOther(CreateUri.For<HomeResource>());
        }
    }
}