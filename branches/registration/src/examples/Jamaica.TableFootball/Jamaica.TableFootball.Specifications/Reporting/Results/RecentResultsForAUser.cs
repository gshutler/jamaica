using System;
using System.Collections.Generic;
using System.Linq;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Reporting;
using Jamaica.Test;
using NUnit.Framework;
using Rhino.Mocks;

namespace Jamaica.TableFootball.Specifications.Reporting.Results
{
    public class RecentResultsForAUser : IntegrationSpecification
    {
        User nathanTyson;
        User robEarnshaw;
        IEnumerable<UserRecentResult> recentResults;

        protected override void Given()
        {
            nathanTyson = session.SaveUser("NathanTyson");
            robEarnshaw = session.SaveUser("RobEarnshaw");

            session.SaveMatchResult(DateTime.Today, nathanTyson, 10, robEarnshaw, 7);
            session.SaveMatchResult(DateTime.Today, robEarnshaw, 10, nathanTyson, 6);
            session.SaveMatchResult(DateTime.Today, robEarnshaw, 10, nathanTyson, 7);
            session.SaveMatchResult(1.DaysAgo(), nathanTyson, 10, robEarnshaw, 1);
            session.SaveMatchResult(4.DaysAgo(), nathanTyson, 10, robEarnshaw, 7);
            session.SaveMatchResult(5.DaysAgo(), nathanTyson, 10, robEarnshaw, 9);
            session.SaveMatchResult(5.DaysAgo(), nathanTyson, 10, robEarnshaw, 4);

            session.Flush();
            session.Clear();
        }

        protected override void When()
        {
            recentResults = Subject<ResultReportingService>().RecentResults(nathanTyson);
        }

        [Then]
        public void FiveResultsReturned()
        {
            Verify(recentResults.Count(), Is.EqualTo(5));
        }

        [Then]
        public void ThreeResultsFromToday()
        {
            Verify(recentResults.Where(result => result.MatchDate == DateTime.Today).Count(), Is.EqualTo(3));
        }

        [Then]
        public void OneResultFromYesterday()
        {
            Verify(recentResults.Where(result => result.MatchDate == DateTime.Today.AddDays(-1)).Count(), Is.EqualTo(1));
        }

        [Then]
        public void OneResultFromFourDaysAgo()
        {
            Verify(recentResults.Where(result => result.MatchDate == DateTime.Today.AddDays(-4)).Count(), Is.EqualTo(1));
        }

        [Then]
        public void AllResultsInDescendingDateOrder()
        {
            var lastDate = DateTime.MaxValue;

            foreach (var result in recentResults)
            {
                Verify(result.MatchDate, Is.LessThanOrEqualTo(lastDate));
                lastDate = result.MatchDate;
            }
        }

        [Then]
        public void ThreeWins()
        {
            var wins = recentResults.OfType<UserRecentWin>().ToList();

            Verify(wins.Count, Is.EqualTo(3));

            VerifyResult(wins[0], 10, 7, robEarnshaw);
            VerifyResult(wins[1], 10, 1, robEarnshaw);
            VerifyResult(wins[2], 10, 7, robEarnshaw);
        }

        [Then]
        public void TwoLosses()
        {
            var losses = recentResults.OfType<UserRecentLoss>().ToList();

            Verify(losses.Count, Is.EqualTo(2));

            VerifyResult(losses[0], 6, 10, robEarnshaw);
            VerifyResult(losses[1], 7, 10, robEarnshaw);
        }

        void VerifyResult(UserRecentResult recentResult, int userScore, int opponentScore, ISecurityPrincipal opponent)
        {
            Verify(recentResult.OpponentName, Is.EqualTo(opponent.Name));
            Verify(recentResult.UserScore, Is.EqualTo(userScore));
            Verify(recentResult.OpponentScore, Is.EqualTo(opponentScore));
        }
    }
}