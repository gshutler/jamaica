using System;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Recording;
using Jamaica.TableFootball.Core.Recording.VictoryRecording;
using Jamaica.TableFootball.Core.Xhtml;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.RecordingScores.RecordingAVictory
{
    public class RequestingAForm : Specification
    {
        OperationResult result;
        ISecurityPrincipal user;
        SelectList opponentList;
        SelectList scoreList;
        SelectList dateList;

        protected override void Given()
        {
            user = new User("NathanTyson");
            opponentList = new SelectList();
            scoreList = new SelectList();
            dateList = new SelectList();

            InjectDependency(user);

            Dependency<IScoringSelectListService>()
                .Stub(x => x.OtherPlayers(user))
                .Return(opponentList);

            Dependency<IScoringSelectListService>()
                .Stub(x => x.PossibleOpponentScores())
                .Return(scoreList);

            Dependency<IScoringSelectListService>()
                .Stub(x => x.MatchDates())
                .Return(dateList);
        }

        protected override void When()
        {
            result = Subject<VictoryRecordingHandler>().Get();
        }

        [Then]
        public void ResponseIsOK()
        {
            Verify(result, Is.InstanceOf<OperationResult.OK>());
        }

        [Then]
        public void PassedPotentialOpponents()
        {
            Verify(result.Response<VictoryRecordingResource>().Opponents, Is.SameAs(opponentList));
        }

        [Then]
        public void PassedPotentialScoresForTheOpponent()
        {
            Verify(result.Response<VictoryRecordingResource>().Scores, Is.SameAs(scoreList));
        }

        [Then]
        public void PassedPotentialMatchDates()
        {
            Verify(result.Response<VictoryRecordingResource>().Dates, Is.SameAs(dateList));
        }
    }
}