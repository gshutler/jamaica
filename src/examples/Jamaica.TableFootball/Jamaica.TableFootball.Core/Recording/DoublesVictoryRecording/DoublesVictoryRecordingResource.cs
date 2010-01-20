using System;
using Jamaica.TableFootball.Core.Xhtml;

namespace Jamaica.TableFootball.Core.Recording.DoublesVictoryRecording
{
    public class DoublesVictoryRecordingResource
    {
        public DoublesVictoryRecordingResource()
        {
            PartnerId = "";
            FirstOpponentId = "";
            SecondOpponentId = "";
        }

        public SelectList OtherPlayers { get; set; }
        public SelectList Scores { get; set; }
        public SelectList Dates { get; set; }
        public string PartnerId { get; set; }
        public string FirstOpponentId { get; set; }
        public string SecondOpponentId { get; set; }
    }
}