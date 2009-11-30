using System;
using Jamaica.Security;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core.Home
{
    public class HomeHandler : Handler
    {
        readonly ISecurityPrincipal securityPrincipal;

        public HomeHandler(ISecurityPrincipal securityPrincipal)
        {
            this.securityPrincipal = securityPrincipal;
        }

        public OperationResult Get()
        {
            var resource = new HomeResource(securityPrincipal);

            return OK(resource);
        }
    }
}