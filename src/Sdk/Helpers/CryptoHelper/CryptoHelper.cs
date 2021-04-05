using System.Security.Cryptography;
using System.Text;

namespace Sdk.Helpers.CryptoHelper
{
    public class CryptoHelper : ICryptoHelper
    {
        public string HashWithSha256(string data)
        {
            var sha256 = SHA256.Create();

            sha256.ComputeHash(Encoding.UTF8.GetBytes(data));

            var stringBuilder = new StringBuilder();

            foreach (byte t in sha256.Hash)
                stringBuilder.Append(t.ToString("x2"));

            return stringBuilder.ToString();
        }
    }
}
