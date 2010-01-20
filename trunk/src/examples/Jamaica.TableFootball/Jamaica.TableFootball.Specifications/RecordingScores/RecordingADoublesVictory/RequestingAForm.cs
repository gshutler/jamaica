using System;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Recording;
using Jamaica.TableFootball.Core.Recording.DoublesVictoryRecording;
using Jamaica.TableFootball.Core.Xhtml;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.RecordingScores.RecordingADoublesVictory
{
    public class RequestingAForm : Specification
    {
        OperationResult result;
        readonly ISecurityPrincipal user = new User("NathanTyson");
        readonly SelectList playerList = new SelectList();
        readonly SelectList scoresList = new SelectList();
        readonly SelectList matchDatesList = new SelectList();

        protected override void Given()
        {
            InjectDependency(user);

            Dependency<IScoringSelectListService>()
                .Stub(x => x.OtherPlayers(user))
                .Return(playerList);

            Dependency<IScoringSelectListService>()
                .Stub(x => x.PossibleOpponentScores())
                .Return(scoresList);

            Dependency<IScoringSelectListService>()
                .Stub(x => x.MatchDates())
                .Return(matchDatesList);
        }

        protected override void When()
        {
            result = Subject<DoublesVictoryRecordingHandler>().Get();
        }

        [Then]
        public void ListOfOtherPlayersSupplied()
        {
            Verify(result.Response<DoublesVictoryRecordingResource>().OtherPlayers, Is.SameAs(playerList));
        }

        [Then]
        public void ListOfPossibleOpponentScoresSupplied()
        {
            Verify(result.Response<DoublesVictoryRecordingResource>().Scores, Is.SameAs(scoresList));
        }

        [Then]
        public void ListOfMatchDatesSupplied()
        {
            Verify(result.Response<DoublesVictoryRecordingResource>().Dates, Is.SameAs(matchDatesList));
        }

        [Then]
        public void PartnerIsNotSelected()
        {
            Verify(result.Response<DoublesVictoryRecordingResource>().PartnerId, Is.EqualTo(""));
        }

        [Then]
        public void FirstOpponentIsNotSelected()
        {
            Verify(result.Response<DoublesVictoryRecordingResource>().FirstOpponentId, Is.EqualTo(""));
        }

        [Then]
        public void SecondOpponentIsNotSelected()
        {
            Verify(result.Response<DoublesVictoryRecordingResource>().SecondOpponentId, Is.EqualTo(""));
        }
    }
}