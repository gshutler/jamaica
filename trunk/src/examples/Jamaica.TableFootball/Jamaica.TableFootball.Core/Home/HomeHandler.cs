using System;
using System.Collections;
using System.Collections.Generic;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Reporting;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core.Home
{
    public class HomeHandler : Handler
    {
        readonly ISecurityPrincipal securityPrincipal;
        readonly IResultReportingService resultReportingService;

        public HomeHandler(ISecurityPrincipal securityPrincipal, IResultReportingService resultReportingService)
        {
            this.securityPrincipal = securityPrincipal;
            this.resultReportingService = resultReportingService;
        }

        public OperationResult Get()
        {
            IEnumerable<UserRecentResult> recentResults = null;
            
            if (securityPrincipal != User.Anonymous)
            {
                recentResults = resultReportingService.RecentResults(securityPrincipal);
            }

            var resource = new HomeResource(securityPrincipal, recentResults);

            return OK(resource);
        }
    }
}