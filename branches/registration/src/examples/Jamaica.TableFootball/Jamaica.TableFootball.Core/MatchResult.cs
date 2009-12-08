using System;

namespace Jamaica.TableFootball.Core
{
    public class MatchResult
    {
        public MatchResult(DateTime date, Participant victor, Participant opponent)
        {
            Date = date;
            Victor = victor;
            Opponent = opponent;
        }

        protected MatchResult()
        {
        }

        public virtual int Id { get; protected set; }
        public virtual DateTime Date { get; protected set; }
        public virtual Participant Victor { get; protected set; }
        public virtual Participant Opponent { get; protected set; }
    }
}