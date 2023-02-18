
using System.Security.Cryptography;
using System.Text;

namespace FORUMAPPLICATION.SERVICELAYER
{
     public class SHA256HashGenerator
    {
        public static string GenerateHash(string inputData)
        {
            using (SHA256 sha256Hash= SHA256.Create())
            {
                byte[] bytes=sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(inputData));
                StringBuilder builder= new StringBuilder();
                for (int i=0;i<bytes.Length;i++) 
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
