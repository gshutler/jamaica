using System;
using System.Collections.Generic;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Xhtml;
using NHibernate;
using NHibernate.Criterion;

namespace Jamaica.TableFootball.Core.Recording
{
    public interface IScoringSelectListService
    {
        SelectList OtherPlayers(ISecurityPrincipal user);
        SelectList PossibleOpponentScores();
        SelectList MatchDates();
    }

    public class ScoringSelectListService : IScoringSelectListService
    {
        readonly ISession session;

        public ScoringSelectListService(ISession session)
        {
            this.session = session;
        }

        public SelectList OtherPlayers(ISecurityPrincipal user)
        {
            var opponents = session.CreateCriteria<User>()
                .Add(Restrictions.Not(Restrictions.Eq("Name", user.Name)))
                .AddOrder(Order.Asc("Name"))
                .List<User>();

            return new SelectList(opponents.AsSelectListItems());
        }

        public SelectList PossibleOpponentScores()
        {
            return new SelectList(ScoreSelectListItems());
        }

        static IEnumerable<SelectListItem> ScoreSelectListItems()
        {
            for (var score = 0; score < 10; score++)
            {
                yield return new SelectListItem(score.ToString());
            }
        }

        public SelectList MatchDates()
        {
            return new SelectList(MatchDateSelectListItems());
        }

        static IEnumerable<SelectListItem> MatchDateSelectListItems()
        {
            yield return new SelectListItem("Today", DateSelectValue(DateTime.Today));
            yield return new SelectListItem("Yesterday", DateSelectValue(DateTime.Today.AddDays(-1)));

            for (var dateOffset = -2; dateOffset > -5; dateOffset--)
            {
                var date = DateTime.Today.AddDays(dateOffset);
                yield return new SelectListItem(date.ToString("dddd"), DateSelectValue(date));
            }
        }

        static string DateSelectValue(DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }
    }
}