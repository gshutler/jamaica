using System;
using System.Linq;
using Jamaica.TableFootball.Core.Recording;
using Jamaica.TableFootball.Core.Xhtml;
using NUnit.Framework;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.RecordingScores.ScoringSelectLists
{
    public class MatchDateList : Specification
    {
        SelectList matchDates;

        protected override void Given()
        {
        }

        protected override void When()
        {
            matchDates = Subject<ScoringSelectListService>().MatchDates();
        }

        [Then]
        public void HasFiveOptions()
        {
            Verify(matchDates.Items.Count(), Is.EqualTo(5));
        }

        [Then]
        public void HasExpectedListFromTodayBackFourDays()
        {
            var date = DateTime.Today;
            var dateIndex = 0;

            VerifyDisplayAndValue(dateIndex++, "Today", date.ToString("yyyyMMdd"));

            date = DateTime.Today.AddDays(-dateIndex);
            VerifyDisplayAndValue(dateIndex++, "Yesterday", date.ToString("yyyyMMdd"));
            
            date = DateTime.Today.AddDays(-dateIndex);
            VerifyDisplayAndValue(dateIndex++, date.ToString("dddd"), date.ToString("yyyyMMdd"));
            
            date = DateTime.Today.AddDays(-dateIndex);
            VerifyDisplayAndValue(dateIndex++, date.ToString("dddd"), date.ToString("yyyyMMdd"));
            
            date = DateTime.Today.AddDays(-dateIndex);
            VerifyDisplayAndValue(dateIndex, date.ToString("dddd"), date.ToString("yyyyMMdd"));
        }
        
        void VerifyDisplayAndValue(int date, string display, string value)
        {
            var matchDate = matchDates.Items.Skip(date).First();

            Verify(matchDate.Display, Is.EqualTo(display));
            Verify(matchDate.Value, Is.EqualTo(value));
        }
    }
}