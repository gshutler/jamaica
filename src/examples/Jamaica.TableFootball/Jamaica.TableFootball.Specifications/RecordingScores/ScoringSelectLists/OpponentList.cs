using System;
using System.Linq;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Recording;
using Jamaica.TableFootball.Core.Xhtml;
using Jamaica.Test;
using NUnit.Framework;
using Rhino.Mocks;

namespace Jamaica.TableFootball.Specifications.RecordingScores.ScoringSelectLists
{
    public class OpponentList : IntegrationSpecification
    {
        User tyson;
        SelectList opponents;

        protected override void Given()
        {
            session.Save(tyson = new User("Tyson"));
            session.Save(new User("Morgan"));
            session.Save(new User("Anderson"));
            session.Save(new User("Moussi"));

            session.Flush();
        }

        protected override void When()
        {
            opponents = Subject<ScoringSelectListService>().Opponents(tyson);
        }

        [Then]
        public void FourItemsInSelectList()
        {
            Verify(opponents.Items.Count(), Is.EqualTo(4));
        }

        [Then]
        public void DefaultPlusOpponentsInAlphabeticalOrderNotIncludingOriginalPlayer()
        {
            var opponentList = opponents.Items.ToList();

            Verify(opponentList[0].Display, Is.EqualTo("Select opponent"));
            Verify(opponentList[0].Value, Is.EqualTo(""));
            Verify(opponentList[1].Display, Is.EqualTo("Anderson"));
            Verify(opponentList[1].Value, Is.EqualTo("Anderson"));
            Verify(opponentList[2].Display, Is.EqualTo("Morgan"));
            Verify(opponentList[2].Value, Is.EqualTo("Morgan"));
            Verify(opponentList[3].Display, Is.EqualTo("Moussi"));
            Verify(opponentList[3].Value, Is.EqualTo("Moussi"));
        }
    }
}