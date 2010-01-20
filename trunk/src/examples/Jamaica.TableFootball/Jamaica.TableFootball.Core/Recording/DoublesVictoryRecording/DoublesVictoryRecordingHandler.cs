using System;
using Jamaica.Security;
using OpenRasta.Web;

namespace Jamaica.TableFootball.Core.Recording.DoublesVictoryRecording
{
    public class DoublesVictoryRecordingHandler : Handler
    {
        readonly IScoringSelectListService scoringSelectListService;
        readonly ISecurityPrincipal securityPrincipal;

        public DoublesVictoryRecordingHandler(ISecurityPrincipal securityPrincipal, IScoringSelectListService scoringSelectListService)
        {
            this.scoringSelectListService = scoringSelectListService;
            this.securityPrincipal = securityPrincipal;
        }

        public OperationResult Get()
        {
            var playerList = scoringSelectListService.OtherPlayers(securityPrincipal);
            var scoresList = scoringSelectListService.PossibleOpponentScores();
            var datesList = scoringSelectListService.MatchDates();

            var resource = new DoublesVictoryRecordingResource
                               {
                                   OtherPlayers = playerList,
                                   Scores = scoresList,
                                   Dates = datesList
                               };

            return OK(resource);
        }
    }
}