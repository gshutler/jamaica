using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jamaica.Security;
using NHibernate;
using NHibernate.Criterion;

namespace Jamaica.TableFootball.Core.Reporting
{
    public interface IStatisticsReportingService
    {
        UserStatisticsSummary SummaryFor(ISecurityPrincipal securityPrincipal);
        IEnumerable<UserStatisticsSummary> LeagueTable();
    }

    public class StatisticsReportingService : IStatisticsReportingService
    {
        const string StatisticsSummaryQuery = @"
                 SELECT u as User,
                        (SELECT COUNT(win) FROM MatchResult win WHERE win.Victor.User = u) AS Wins,
                        (SELECT COUNT(loss) FROM MatchResult loss WHERE loss.Opponent.User = u) AS Losses,
                        (SELECT SUM(win.Victor.Score) FROM MatchResult win WHERE win.Victor.User = u) AS ScoredInVictories,
                        (SELECT SUM(loss.Opponent.Score) FROM MatchResult loss WHERE loss.Opponent.User = u) AS ScoredInDefeats,
                        (SELECT SUM(win.Opponent.Score) FROM MatchResult win WHERE win.Victor.User = u) AS ConcededInVictories,
                        (SELECT SUM(loss.Victor.Score) FROM MatchResult loss WHERE loss.Opponent.User = u) AS ConcededInDefeats
                 FROM   User u";

        enum Summary
        {
            User = 0,
            Wins,
            Losses,
            ScoredInVictories,
            ScoredInLosses,
            ConcededInVictories,
            ConcededInDefeats
        }

        readonly ISession session;

        public StatisticsReportingService(ISession session)
        {
            this.session = session;
        }

        public UserStatisticsSummary SummaryFor(ISecurityPrincipal securityPrincipal)
        {
            var summary = session.CreateQuery(StatisticsSummaryQuery + @"
                 WHERE  u.Name = :name")
                .SetString("name", securityPrincipal.Name)
                .UniqueResult();

            return ConvertToUserStatisticsSummary(summary);
        }

        public IEnumerable<UserStatisticsSummary> LeagueTable()
        {
            var summaries = session.CreateQuery(StatisticsSummaryQuery).List();

            return ConvertToUserStatisticsSummaries(summaries)
                .OrderByDescending(summary => summary.WinPercentage)
                .ThenByDescending(summary => summary.GoalsScored);
        }

        static IEnumerable<UserStatisticsSummary> ConvertToUserStatisticsSummaries(IList summaries)
        {
            foreach (var summary in summaries)
            {
                yield return ConvertToUserStatisticsSummary(summary);
            }
        }

        static UserStatisticsSummary ConvertToUserStatisticsSummary(object summary)
        {
            var values = (object[]) summary;
            
            return new UserStatisticsSummary(
                (User) values[(int) Summary.User],
                Retrieve(values, Summary.Wins), 
                Retrieve(values, Summary.Losses),
                Retrieve(values, Summary.ScoredInVictories) + Retrieve(values, Summary.ScoredInLosses),
                Retrieve(values, Summary.ConcededInVictories) + Retrieve(values, Summary.ConcededInDefeats));
        }

        static int Retrieve(object[] values, Summary index)
        {
            return Convert.ToInt32(values[(int) index]);
        }
    }
}