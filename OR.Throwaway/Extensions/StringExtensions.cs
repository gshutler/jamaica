using System;
using System.Text.RegularExpressions;

namespace OR.Throwaway.Extensions
{
    public static class StringExtensions
    {
        public static bool Set(this string value)
        {
            return !Empty(value);
        }

        public static bool Empty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string[] AsTags(this string tagList)
        {
            return Regex.Replace(tagList.ToLower(), @"[^ \-0-9a-z]", "")
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}