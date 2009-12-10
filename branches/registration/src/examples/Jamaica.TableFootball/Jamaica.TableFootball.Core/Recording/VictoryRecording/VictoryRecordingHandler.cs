using System;
using System.Globalization;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Extensions;
using Jamaica.TableFootball.Core.Home;
using NHibernate;
using OpenRasta.Security;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core.Recording.VictoryRecording
{
    [RequiresAuthentication]
    [Uri("/victories/new")]
    public class VictoryRecordingHandler : Handler
    {
        readonly ISecurityPrincipal securityPrincipal;
        readonly IScoringSelectListService scoringSelectListService;
        readonly ISession session;

        public VictoryRecordingHandler(ISecurityPrincipal securityPrincipal, IScoringSelectListService scoringSelectListService, ISession session)
        {
            this.securityPrincipal = securityPrincipal;
            this.scoringSelectListService = scoringSelectListService;
            this.session = session;
        }

        public OperationResult Get()
        {
            var resource = new VictoryRecordingResource();

            AttachSelectLists(resource);

            return OK(resource);
        }

        public OperationResult Post(VictoryRecordingResource resource)
        {
            if (resource.OpponentId.Provided() && resource.OpponentScore.Provided())
            {
                SaveMatchResult(resource);
                return SeeOther<HomeResource>();
            }

            SetErrorMessageAndAttachSelectLists(resource);
            return BadRequest(resource);
        }

        void SetErrorMessageAndAttachSelectLists(VictoryRecordingResource resource)
        {
            if (!resource.OpponentId.Provided())
            {
                resource.SetErrorMessage("You must select an opponent");
            }
            else
            {
                resource.SetErrorMessage("You must select a score for your opponent");
            }

            AttachSelectLists(resource);
        }

        void SaveMatchResult(VictoryRecordingResource resource)
        {
            var victor = session.Load<User>(securityPrincipal.Name);
            var opponent = session.Get<User>(resource.OpponentId);
            var opponentScore = int.Parse(resource.OpponentScore);
            var matchDate = ParseMatchDate(resource);
            
            var matchResult = MatchResult(matchDate, victor, 10, opponent, opponentScore);

            session.Save(matchResult);
        }

        static DateTime ParseMatchDate(VictoryRecordingResource resource)
        {
            return DateTime.ParseExact(resource.MatchDate, "yyyyMMdd",
                                       CultureInfo.InvariantCulture.DateTimeFormat);
        }

        static MatchResult MatchResult(DateTime matchDate, User victor, int victorScore, User opponent, int opponentScore)
        {
            var victorParticipant = new Participant(victor, victorScore);
            var opponentParticipant = new Participant(opponent, opponentScore);

            return new MatchResult(matchDate, victorParticipant, opponentParticipant);
        }

        void AttachSelectLists(VictoryRecordingResource resource)
        {
            resource.Dates = scoringSelectListService.MatchDates();
            resource.Opponents = scoringSelectListService.Opponents(securityPrincipal);
            resource.Scores = scoringSelectListService.PossibleOpponentScores();
        }
    }
}