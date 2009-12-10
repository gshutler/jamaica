using System;
using Jamaica.Services;
using Jamaica.TableFootball.Core.Home;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core.Authentication.Logout
{
    [IgnoreConvention]
    public class LogoutHandler : Handler
    {
        readonly ICookieAuthenticationService cookieAuthenticationService;

        public LogoutHandler(ICookieAuthenticationService cookieAuthenticationService)
        {
            this.cookieAuthenticationService = cookieAuthenticationService;
        }

        public OperationResult Get()
        {
            cookieAuthenticationService.ClearCookies();
            return SeeOther<HomeResource>();
        }
    }
}