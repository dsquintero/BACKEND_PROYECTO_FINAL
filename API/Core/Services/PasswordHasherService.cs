using API.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace API.Core.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public string? SHA1(string password)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        public bool VerifyPassword(string password, string passwordHash)
        {
            return Equals(SHA1(password), passwordHash);
        }

    }
}
