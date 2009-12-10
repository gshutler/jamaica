using System;
using Jamaica.TableFootball.Core.Xhtml;

namespace Jamaica.TableFootball.Core.Recording.VictoryRecording
{
    public class VictoryRecordingResource
    {
        public SelectList Dates { get; set; }
        public SelectList Opponents { get; set; }
        public SelectList Scores { get; set; }

        public string MatchDate { get; set; }
        public string OpponentId { get; set; }
        public string OpponentScore { get; set; }

        public bool SubmissionFailed { get; private set; }
        public string ErrorMessage { get; private set; }

        public void SetErrorMessage(string errorMessage)
        {
            SubmissionFailed = true;
            ErrorMessage = errorMessage;
        }
    }
}