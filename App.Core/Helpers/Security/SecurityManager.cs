using System.Security.Cryptography;
using System.Text;

namespace App.Core.Helpers.Security
{
    public class SecurityManager
    {
        public static string EncryptPassword(string password)
        {
            var sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] bytes = enc.GetBytes(password);
                byte[] result = hash.ComputeHash(bytes);

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}