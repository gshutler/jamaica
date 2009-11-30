using System;
using Jamaica.Security;

namespace Jamaica.TableFootball.Core.Home
{
    public class HomeResource
    {
        public HomeResource(ISecurityPrincipal securityPrincipal)
        {
            SecurityPrincipal = securityPrincipal;
        }

        public ISecurityPrincipal SecurityPrincipal { get; private set; }

        public bool Authenticated
        {
            get { return SecurityPrincipal != User.Anonymous; }
        }
    }
}