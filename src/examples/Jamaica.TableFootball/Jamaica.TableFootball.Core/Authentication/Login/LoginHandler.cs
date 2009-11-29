using System;
using Jamaica.Security;
using Jamaica.Services;
using Jamaica.TableFootball.Core.Home;
using NHibernate;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core.Authentication.Login
{
    public class LoginHandler : Handler
    {
        readonly ISession session;
        readonly ICookieAuthenticationService cookieAuthenticationService;

        public LoginHandler(ISession session, ICookieAuthenticationService cookieAuthenticationService)
        {
            this.session = session;
            this.cookieAuthenticationService = cookieAuthenticationService;
        }

        public OperationResult Get()
        {
            return OK<LoginResource>();
        }

        public OperationResult Post(LoginResource loginResource)
        {
            var user = session.Get<User>(loginResource.Name);

            if (user != null && user.VerifyPassword(loginResource.Password))
            {
                cookieAuthenticationService.AuthorizeSecurityPrincipal(user);
                return SeeOther<HomeResource>();
            }

            var errorMessage = user == null ? "Name not recognised" : "Incorrect password";

            loginResource.SetErrorMessage(errorMessage);

            return BadRequest(loginResource);
        }
    }
}