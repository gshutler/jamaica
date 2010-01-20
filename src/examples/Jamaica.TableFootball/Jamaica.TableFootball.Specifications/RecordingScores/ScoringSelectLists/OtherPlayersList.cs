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
    public class OtherPlayersList : IntegrationSpecification
    {
        User tyson;
        SelectList otherPlayers;

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
            otherPlayers = Subject<ScoringSelectListService>().OtherPlayers(tyson);
        }

        [Then]
        public void ThreeOtherPlayersInSelectList()
        {
            Verify(otherPlayers.Items.Count(), Is.EqualTo(3));
        }

        [Then]
        public void OtherPlayersInAlphabeticalOrderNotIncludingOriginalPlayer()
        {
            var items = otherPlayers.Items.ToList();

            Verify(items[0].Display, Is.EqualTo("Anderson"));
            Verify(items[0].Value, Is.EqualTo("Anderson"));
            Verify(items[1].Display, Is.EqualTo("Morgan"));
            Verify(items[1].Value, Is.EqualTo("Morgan"));
            Verify(items[2].Display, Is.EqualTo("Moussi"));
            Verify(items[2].Value, Is.EqualTo("Moussi"));
        }
    }
}