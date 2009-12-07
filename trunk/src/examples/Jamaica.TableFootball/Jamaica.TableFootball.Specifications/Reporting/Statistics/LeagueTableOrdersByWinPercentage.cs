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
    public class LeagueTableOrdersByWinPercentage : IntegrationSpecification
    {
        User nathanTyson;
        User robEarnshaw;
        User paulAnderson;
        IList<UserStatisticsSummary> table;

        protected override void Given()
        {
            nathanTyson = session.SaveUser("NathanTyson");
            robEarnshaw = session.SaveUser("RobEarnshaw");
            paulAnderson = session.SaveUser("PaulAnderson");

            session.SaveMatchResult(DateTime.Today, nathanTyson, 10, robEarnshaw, 5);
            session.SaveMatchResult(2.DaysAgo(), robEarnshaw, 10, nathanTyson, 6);
            session.SaveMatchResult(4.DaysAgo(), nathanTyson, 10, robEarnshaw, 9);
            session.SaveMatchResult(3.DaysAgo(), paulAnderson, 10, nathanTyson, 4);
            session.SaveMatchResult(1.DaysAgo(), nathanTyson, 10, paulAnderson, 8);

            // Nathan Tyson:  W3 L2 F40 A42
            // Paul Anderson: W1 L1 F18 A14
            // Rob Earnshaw:  W1 L2 F24 A26
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
        public void PaulAndersonSecond()
        {
            Verify(table[1].User, Is.SameAs(paulAnderson));
        }

        [Then]
        public void RobEarnshawIsThird()
        {
            Verify(table[2].User, Is.SameAs(robEarnshaw));
        }
    }
}