using System.Security.Cryptography;
using System.Text;

namespace App.Core.Helpers.Security
{
    public class SecurityManager
    {
        public static string EncryptPassword(string password)
        {
            var sb = new StringBuilder();
            Encoding enc = Encoding.UTF8;
            byte[] bytes = enc.GetBytes(password);
            byte[] result = SHA256.HashData(bytes);

            foreach (byte b in result)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}