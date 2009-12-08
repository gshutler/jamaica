using System;
using System.Collections.Generic;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Home;
using Jamaica.TableFootball.Core.Reporting;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Home
{
    public class ViewingTheHomePageAsAnAuthenticatedUser : Specification
    {
        User user;
        OperationResult result;
        IEnumerable<UserRecentResult> recentResults;
        List<UserStatisticsSummary> leagueTable;

        protected override void Given()
        {
            leagueTable = new List<UserStatisticsSummary>();
            user = new User("NathanTyson");
            user.SetPassword("forest");

            recentResults = new List<UserRecentResult>();

            InjectDependency<ISecurityPrincipal>(user);

            Dependency<IResultReportingService>()
                .Stub(x => x.RecentResults(user))
                .Return(recentResults);

            Dependency<IStatisticsReportingService>()
                .Stub(x => x.LeagueTable())
                .Return(leagueTable);
        }

        protected override void When()
        {
            result = Subject<HomeHandler>().Get();
        }

        [Then]
        public void ResponseIsOK()
        {
            Verify(result, Is.InstanceOf<OperationResult.OK>());
        }

        [Then]
        public void ReportsAsAuthenticated()
        {
            Verify(result.Response<HomeResource>().Authenticated, Is.True);
        }

        [Then]
        public void SecurityPrincipalIsAuthenticatedUser()
        {
            Verify(result.Response<HomeResource>().SecurityPrincipal, Is.SameAs(user));
        }

        [Then]
        public void ShowsUsersMostRecentResults()
        {
            Verify(result.Response<HomeResource>().RecentResults, Is.SameAs(recentResults));
        }

        [Then]
        public void LeagueTablePassed()
        {
            Verify(result.Response<HomeResource>().LeagueTable, Is.SameAs(leagueTable));
        }
    }
}