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
        }

        public User User { get; private set; }
        public int Wins { get; private set; }
        public int Losses { get; private set; }
        public int GoalsScored { get; private set; }
        public int GoalsConceded { get; private set; }

        public int TotalGames
        {
            get { return Wins + Losses; }
        }

        public decimal WinPercentage
        {
            get 
            { 
                return decimal.Round(
                    decimal.Divide(Wins, TotalGames) * 100, 2); 
            }
        }
    }
}