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
    public class NoOpponentSelected : Specification
    {
        VictoryRecordingResource resource;
        OperationResult result;
        SelectList opponentList;
        ISecurityPrincipal user;
        SelectList scoreList;
        SelectList dateList;

        protected override void Given()
        {
            user = new User("NathanTyson");
            opponentList = new SelectList();
            scoreList = new SelectList();
            dateList = new SelectList();

            resource = new VictoryRecordingResource
                           {
                               OpponentId = "",
                               OpponentScore = "7"
                           };

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
            result = Subject<VictoryRecordingHandler>().Post(resource);
        }

        [Then]
        public void ResponseIsBadRequest()
        {
            Verify(result, Is.InstanceOf<OperationResult.BadRequest>());
        }

        [Then]
        public void ResourceIsReturnedToTheUser()
        {
            Verify(result.ResponseResource, Is.SameAs(resource));
        }

        [Then]
        public void SubmissionFailed()
        {
            Verify(result.Response<VictoryRecordingResource>().SubmissionFailed, Is.True);
        }

        [Then]
        public void ErrorMessageForMissingOpponent()
        {
            Verify(result.Response<VictoryRecordingResource>().ErrorMessage, Is.EqualTo("You must select an opponent"));
        }

        [Then]
        public void PotentialOpponentsPassed()
        {
            Verify(result.Response<VictoryRecordingResource>().Opponents, Is.SameAs(opponentList));
        }

        [Then]
        public void PossibleScoresPassed()
        {
            Verify(result.Response<VictoryRecordingResource>().Scores, Is.SameAs(scoreList));
        }

        [Then]
        public void MatchDatesPassed()
        {
            Verify(result.Response<VictoryRecordingResource>().Dates, Is.SameAs(dateList));
        }
    }
}