using System;
using System.Collections.Generic;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Reporting;

namespace Jamaica.TableFootball.Core.Home
{
    public class HomeResource
    {
        public HomeResource(ISecurityPrincipal securityPrincipal, IEnumerable<UserRecentResult> recentResults)
        {
            SecurityPrincipal = securityPrincipal;
            RecentResults = recentResults;
        }

        public IEnumerable<UserRecentResult> RecentResults { get; private set; }
        public ISecurityPrincipal SecurityPrincipal { get; private set; }

        public bool Authenticated
        {
            get { return SecurityPrincipal != User.Anonymous; }
        }
    }
}