using System;

namespace Jamaica.TableFootball.Core.Reporting
{
    public abstract class UserRecentResult
    {
        protected UserRecentResult(DateTime matchDate, string opponentName, int userScore, int opponentScore)
        {
            MatchDate = matchDate;
            OpponentName = opponentName;
            UserScore = userScore;
            OpponentScore = opponentScore;
        }

        public DateTime MatchDate { get; private set; }
        public int UserScore { get; private set; }
        public string OpponentName { get; private set; }
        public int OpponentScore { get; private set; }

        public abstract string Verb();
    }

    public class UserRecentWin : UserRecentResult
    {
        public UserRecentWin(MatchResult matchResult)
            : base(matchResult.Date, matchResult.Opponent.User.Name, matchResult.Victor.Score, matchResult.Opponent.Score)
        {
        }

        public override string Verb()
        {
            return "Won";
        }
    }

    public class UserRecentLoss : UserRecentResult
    {
        public UserRecentLoss(MatchResult matchResult)
            : base(matchResult.Date, matchResult.Victor.User.Name, matchResult.Opponent.Score, matchResult.Victor.Score)
        {
        }

        public override string Verb()
        {
            return "Lost";
        }
    }
}