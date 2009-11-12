using System.Collections.Generic;
using System.Text;

namespace Jamaica.Security
{
    public static class StringExtensions
    {
        public static string ToHash(this string stringToHash)
        {
            var cryptographer = new System.Security.Cryptography.SHA512Managed();
            var hashBytes = cryptographer.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

            return hashBytes.ToHexString();
        }

        private static string ToHexString(this ICollection<byte> bytes)
        {
            var builder = new StringBuilder(bytes.Count * 2);

            foreach (var b in bytes)
            {
                builder.AppendFormat("{0:x2}", b);
            }

            return builder.ToString();
        }
    }
}