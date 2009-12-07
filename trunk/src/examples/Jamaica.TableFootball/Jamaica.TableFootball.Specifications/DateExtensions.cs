using System;

namespace Jamaica.TableFootball.Specifications
{
    public static class DateExtensions
    {
        public static DateTime DaysAgo(this int days)
        {
            return DateTime.Today.AddDays(-days);
        }
    }
}