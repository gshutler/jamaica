using System;
using Jamaica.Security;
using Jamaica.TableFootball.Core;
using NHibernate;

namespace Jamaica.TableFootball.Specifications
{
    public static class SessionExtensions
    {
        public static User SaveUser(this ISession session, string name)
        {
            var user = new User(name);

            session.Save(user);

            return user;
        }

        public static MatchResult SaveMatchResult(this ISession session, DateTime matchDate, User victor, int victorScore, User opponent, int opponentScore)
        {
            var victorParticipant = new Participant(victor, victorScore);
            var opponentParticpant = new Participant(opponent, opponentScore);

            var matchResult = new MatchResult(matchDate, victorParticipant, opponentParticpant);

            session.Save(matchResult);

            return matchResult;
        }
    }
}