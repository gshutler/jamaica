using System;
using System.Linq;
using Jamaica.TableFootball.Core.Recording;
using Jamaica.TableFootball.Core.Xhtml;
using Jamaica.Test;
using NUnit.Framework;

namespace Jamaica.TableFootball.Specifications.RecordingScores.ScoringSelectLists
{
    public class ScoreSelectList : Specification
    {
        SelectList scores;

        protected override void Given()
        {
        }

        protected override void When()
        {
            scores = Subject<ScoringSelectListService>().PossibleOpponentScores();
        }

        [Then]
        public void NumbersZeroToNine()
        {
            var selectItems = scores.Items.ToList();

            for (var score = 0; score < 10; score++)
            {
                Verify(selectItems[score].Display, Is.EqualTo(score.ToString()));
                Verify(selectItems[score].Value, Is.EqualTo(score.ToString()));
            }
        }
    }
}