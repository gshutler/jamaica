using System;
using Jamaica.Security;
using Jamaica.Services;
using Jamaica.TableFootball.Core.Home;
using NHibernate;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core.Authentication.UserRegistration
{
    [Uri("/registration")]
    public class UserRegistrationHandler : Handler
    {
        readonly ISession session;
        readonly ICookieAuthenticationService cookieAuthentication;

        public UserRegistrationHandler(ISession session, ICookieAuthenticationService cookieAuthentication)
        {
            this.session = session;
            this.cookieAuthentication = cookieAuthentication;
        }

        public OperationResult Get()
        {
            return OK<UserRegistrationResource>();
        }

        public OperationResult Post(UserRegistrationResource userRegistrationResource)
        {
            userRegistrationResource.Validate();

            if (userRegistrationResource.Invalid())
            {
                return BadRequest(userRegistrationResource);    
            }

            var user = new User(userRegistrationResource.Name);
            user.SetPassword(userRegistrationResource.Password);

            session.Save(user);
            cookieAuthentication.AuthorizeSecurityPrincipal(user);

            return SeeOther<HomeResource>();
        }
    }
}