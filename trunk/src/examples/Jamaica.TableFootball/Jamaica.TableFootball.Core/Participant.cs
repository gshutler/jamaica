using System;
using Jamaica.Security;

namespace Jamaica.TableFootball.Core
{
    public class Participant
    {
        public Participant(User user, int score)
        {
            User = user;
            Score = score;
        }

        protected Participant()
        {
        }

        public virtual int Id { get; protected set; }
        public virtual User User { get; protected set; }
        public virtual int Score { get; protected set; }
    }
}