using System;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Reporting;
using NUnit.Framework;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Reporting.Statistics
{
    public class CalculatingOnePlayersStats : IntegrationSpecification
    {
        User nathanTyson;
        User robEarnshaw;
        UserStatisticsSummary summary;

        protected override void Given()
        {
            nathanTyson = session.SaveUser("NathanTyson");
            robEarnshaw = session.SaveUser("RobEarnshaw");

            session.SaveMatchResult(DateTime.Today, nathanTyson, 10, robEarnshaw, 5);
            session.SaveMatchResult(2.DaysAgo(), robEarnshaw, 10, nathanTyson, 6);
            session.SaveMatchResult(4.DaysAgo(), nathanTyson, 10, robEarnshaw, 9);
        }

        protected override void When()
        {
            summary = Subject<StatisticsReportingService>().SummaryFor(nathanTyson);
        }

        [Then]
        public void TwoWins()
        {
            Verify(summary.Wins, Is.EqualTo(2));
        }

        [Then]
        public void OneLoss()
        {
            Verify(summary.Losses, Is.EqualTo(1));
        }

        [Then]
        public void WinPercentageTwoThirds()
        {
            Verify(summary.WinPercentage, Is.EqualTo(66.67d));
        }

        [Then]
        public void TwentySixGoalsScored()
        {
            Verify(summary.GoalsScored, Is.EqualTo(26));
        }

        [Then]
        public void TwentyFourGoalsConceded()
        {
            Verify(summary.GoalsConceded, Is.EqualTo(24));
        }
    }
}