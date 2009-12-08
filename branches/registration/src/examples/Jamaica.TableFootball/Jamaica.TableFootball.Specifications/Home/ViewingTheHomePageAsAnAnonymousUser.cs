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
    public class ViewingTheHomePageAsAnAnonymousUser : Specification
    {
        OperationResult result;
        List<UserStatisticsSummary> leagueTable;

        protected override void Given()
        {
            leagueTable = new List<UserStatisticsSummary>();
            
            InjectDependency<ISecurityPrincipal>(User.Anonymous);

            Dependency<IResultReportingService>()
                .Stub(x => x.RecentResults(Arg<ISecurityPrincipal>.Is.Anything))
                .Throw(new InvalidOperationException("Shouldn't be called for anonymous user"));

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
        public void ReportsAsUnauthenticated()
        {
            Verify(result.Response<HomeResource>().Authenticated, Is.False);
        }

        [Then]
        public void SecurityPrincipalIsAnonymousUser()
        {
            Verify(result.Response<HomeResource>().SecurityPrincipal, Is.SameAs(User.Anonymous));
        }

        [Then]
        public void NoRecentResultsRetrieved()
        {
            Verify(result.Response<HomeResource>().RecentResults, Is.Null);
        }

        [Then]
        public void LeagueTablePassed()
        {
            Verify(result.Response<HomeResource>().LeagueTable, Is.SameAs(leagueTable));
        }
    }
}