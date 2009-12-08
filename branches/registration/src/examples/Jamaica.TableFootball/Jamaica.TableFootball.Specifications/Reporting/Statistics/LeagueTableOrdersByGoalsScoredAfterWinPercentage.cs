using System;
using System.Collections.Generic;
using System.Linq;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Reporting;
using NUnit.Framework;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Reporting.Statistics
{
    public class LeagueTableOrdersByGoalsScoredAfterWinPercentage : IntegrationSpecification
    {
        User nathanTyson;
        User robEarnshaw;
        List<UserStatisticsSummary> table;

        protected override void Given()
        {
            robEarnshaw = session.SaveUser("RobEarnshaw");
            nathanTyson = session.SaveUser("NathanTyson");

            session.SaveMatchResult(DateTime.Today, nathanTyson, 10, robEarnshaw, 5);
            session.SaveMatchResult(DateTime.Today, robEarnshaw, 10, nathanTyson, 8);
        }

        protected override void When()
        {
            table = Subject<StatisticsReportingService>().LeagueTable().ToList();
        }

        [Then]
        public void NathanTysonFirst()
        {
            Verify(table[0].User, Is.SameAs(nathanTyson));
        }

        [Then]
        public void RobEarnshawSecond()
        {
            Verify(table[1].User, Is.SameAs(robEarnshaw));
        }
    }
}