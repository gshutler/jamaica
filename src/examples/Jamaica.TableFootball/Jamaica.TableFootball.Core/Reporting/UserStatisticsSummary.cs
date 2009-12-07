using System;
using Jamaica.Security;

namespace Jamaica.TableFootball.Core.Reporting
{
    public class UserStatisticsSummary
    {
        public UserStatisticsSummary(User user, int wins, int losses, int goalsScored, int goalsConceded)
        {
            User = user;
            Wins = wins;
            Losses = losses;
            GoalsScored = goalsScored;
            GoalsConceded = goalsConceded;
            TotalGames = Wins + Losses;
            SetWinPercentage();
        }

        public User User { get; private set; }
        public int Wins { get; private set; }
        public int Losses { get; private set; }
        public int GoalsScored { get; private set; }
        public int GoalsConceded { get; private set; }
        public int TotalGames { get; private set; }
        public decimal WinPercentage { get; private set; }

        void SetWinPercentage()
        {
            if (TotalGames == 0) return; // leave as zero
            WinPercentage = decimal.Round(decimal.Divide(Wins, TotalGames) * 100, 2); 
        }
    }
}