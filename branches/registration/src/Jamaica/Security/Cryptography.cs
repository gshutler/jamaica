using System;
using System.Security.Cryptography;
using System.Text;

namespace Jamaica.Security
{
    public static class Cryptography
    {
        static readonly SHA512Managed Hasher = new SHA512Managed();
        static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();

        public static string GenerateHexHashString(this string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = Hasher.ComputeHash(inputBytes);

            return hashBytes.ToHexString();
        }

        public static string GenerateHexSaltString()
        {
            var bytes = new byte[64];

            RandomNumberGenerator.GetBytes(bytes);

            return bytes.ToHexString();
        }

        private static string ToHexString(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}