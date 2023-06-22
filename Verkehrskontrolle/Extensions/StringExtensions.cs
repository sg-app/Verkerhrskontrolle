using System.Security.Cryptography;

namespace Verkehrskontrolle.Extensions
{
    public static class StringExtensions
    {
        public static string CreateMD5(this string password)
        {
            // Use input string to calculate MD5 hash
            using var md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
