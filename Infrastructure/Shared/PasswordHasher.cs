using Infrastructure.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Shared
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                string hashedPassword = BitConverter.ToString(hashBytes)
                    .Replace("-", "")
                    .ToLower();

                return hashedPassword;
            }
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
