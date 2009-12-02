using System;
using Jamaica.Security;
using Jamaica.TableFootball.Core;
using Jamaica.TableFootball.Core.Home;
using Jamaica.TableFootball.Core.Recording.VictoryRecording;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.RecordingScores.RecordingAVictory
{
    public class ValidSubmission : IntegrationSpecification
    {
        VictoryRecordingResource resource;
        User nathanTyson;
        User krisCommons;
        OperationResult result;

        protected override void Given()
        {
            session.Save(nathanTyson = new User("NathanTyson"));
            session.Save(krisCommons = new User("KrisCommons"));

            resource = new VictoryRecordingResource
                           {
                               OpponentId = "KrisCommons",
                               OpponentScore = "7",
                               MatchDate = DateTime.Today.AddDays(-1).ToString("yyyyMMdd")
                           };

            InjectDependency<ISecurityPrincipal>(nathanTyson);

            UriResolver.Add(new UriRegistration("/home", typeof(HomeResource)));

            session.Flush();
        }

        protected override void When()
        {
            result = Subject<VictoryRecordingHandler>().Post(resource);
        }

        [Then]
        public void RedirectedToHomePage()
        {
            Verify(result, Is.InstanceOf<OperationResult.SeeOther>());
            Verify(result.RedirectLocation, Is.EqualTo(typeof(HomeResource).CreateUri()));
        }

        [Then]
        public void ResultRecorded()
        {
            /* there should only be one match result so we can just get the first one */
            var matchResult = session.CreateCriteria<MatchResult>().UniqueResult<MatchResult>();

            Verify(matchResult.Date, Is.EqualTo(DateTime.Today.AddDays(-1)));
            Verify(matchResult.Victor.User, Is.SameAs(nathanTyson));
            Verify(matchResult.Victor.Score, Is.EqualTo(10));
            Verify(matchResult.Opponent.User, Is.SameAs(krisCommons));
            Verify(matchResult.Opponent.Score, Is.EqualTo(7));
        }
    }
}