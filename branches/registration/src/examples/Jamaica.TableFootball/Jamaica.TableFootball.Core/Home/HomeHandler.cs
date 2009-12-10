using System;
using System.Collections.Generic;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Reporting;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core.Home
{
    [Uri("/")]
    [Uri("/home")]
    public class HomeHandler : Handler
    {
        readonly ISecurityPrincipal securityPrincipal;
        readonly IResultReportingService resultReportingService;
        readonly IStatisticsReportingService statisticsReportingService;

        public HomeHandler(
            ISecurityPrincipal securityPrincipal, 
            IResultReportingService resultReportingService, 
            IStatisticsReportingService statisticsReportingService)
        {
            this.securityPrincipal = securityPrincipal;
            this.resultReportingService = resultReportingService;
            this.statisticsReportingService = statisticsReportingService;
        }

        public OperationResult Get()
        {
            IEnumerable<UserRecentResult> recentResults = null;
            var leagueTable = statisticsReportingService.LeagueTable();
            
            if (securityPrincipal != User.Anonymous)
            {
                recentResults = resultReportingService.RecentResults(securityPrincipal);
            }

            var resource = new HomeResource(securityPrincipal, recentResults, leagueTable);

            return OK(resource);
        }
    }
}