using System;
using System.Collections.Generic;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Reporting;

namespace Jamaica.TableFootball.Core.Home
{
    [UriRegistration("/home", "/")]
    public class HomeResource
    {
        public HomeResource(
            ISecurityPrincipal securityPrincipal, 
            IEnumerable<UserRecentResult> recentResults, 
            IEnumerable<UserStatisticsSummary> leagueTable)
        {
            SecurityPrincipal = securityPrincipal;
            RecentResults = recentResults;
            LeagueTable = leagueTable;
        }

        public ISecurityPrincipal SecurityPrincipal { get; private set; }
        public IEnumerable<UserRecentResult> RecentResults { get; private set; }
        public IEnumerable<UserStatisticsSummary> LeagueTable { get; private set; }

        public bool Authenticated
        {
            get { return SecurityPrincipal != User.Anonymous; }
        }
    }
}