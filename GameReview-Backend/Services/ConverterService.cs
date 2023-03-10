using GameReview_Backend.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace GameReview_Backend.Services
{
    public class ConverterService : IConverter
    {
        public string PasswordEncription(string password)
        {
            string hashedPassword;

            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                hashedPassword = Convert.ToBase64String(hash);
            }

            return hashedPassword;
        }
    }
}
