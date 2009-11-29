namespace Jamaica.TableFootball.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool Provided(this string value)
        {
            return !string.IsNullOrEmpty(value) && value.Trim().Length > 0;
        }
    }
}