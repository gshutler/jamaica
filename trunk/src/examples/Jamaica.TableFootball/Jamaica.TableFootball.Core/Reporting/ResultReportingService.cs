using System;
using System.Collections.Generic;
using Jamaica.Security;
using NHibernate;
using NHibernate.Criterion;

namespace Jamaica.TableFootball.Core.Reporting
{
    public interface IResultReportingService
    {
        IEnumerable<UserRecentResult> RecentResults(ISecurityPrincipal securityPrincipal);
    }

    public class ResultReportingService : IResultReportingService
    {
        readonly ISession session;

        public ResultReportingService(ISession session)
        {
            this.session = session;
        }

        public IEnumerable<UserRecentResult> RecentResults(ISecurityPrincipal securityPrincipal)
        {
            var relevantMatchResult = RecentResultsInvolvingSecurityPrincipal(securityPrincipal);

            return TranslateMatchResultsToUserRecentResults(securityPrincipal, relevantMatchResult);
        }

        IList<MatchResult> RecentResultsInvolvingSecurityPrincipal(ISecurityPrincipal securityPrincipal)
        {
            return session.CreateCriteria<MatchResult>()
                .CreateAlias("Victor", "v").CreateAlias("v.User", "vu")
                .CreateAlias("Opponent", "o").CreateAlias("o.User", "ou")
                .Add(Restrictions.Disjunction()
                         .Add(Restrictions.Eq("vu.Name", securityPrincipal.Name))
                         .Add(Restrictions.Eq("ou.Name", securityPrincipal.Name)))
                .AddOrder(Order.Desc("Date"))
                .SetMaxResults(5)
                .List<MatchResult>();
        }

        static IEnumerable<UserRecentResult> TranslateMatchResultsToUserRecentResults(ISecurityPrincipal securityPrincipal, IEnumerable<MatchResult> relevantMatchResult)
        {
            foreach (var matchResult in relevantMatchResult)
            {
                if (matchResult.Victor.User.Name == securityPrincipal.Name)
                {
                    yield return new UserRecentWin(matchResult);
                }
                else
                {
                    yield return new UserRecentLoss(matchResult);
                }
            }
        }
    }
}