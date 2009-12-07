using System;
using Jamaica.Security;
using Jamaica.TableFootball.Core.Reporting;
using NUnit.Framework;
using Rhino.Mocks;
using Jamaica.Test;

namespace Jamaica.TableFootball.Specifications.Reporting.Statistics
{
    public class HavingNoResultsReportsWinPercentageOfZero : IntegrationSpecification
    {
        User nathanTyson;
        UserStatisticsSummary summary;

        protected override void Given()
        {
            nathanTyson = session.SaveUser("NathanTyson");
            session.Flush();
        }

        protected override void When()
        {
            summary = Subject<StatisticsReportingService>().SummaryFor(nathanTyson);
        }

        [Then]
        public void WinPercentageIsZero()
        {
            Verify(summary.WinPercentage, Is.EqualTo(0d));
        }
    }
}