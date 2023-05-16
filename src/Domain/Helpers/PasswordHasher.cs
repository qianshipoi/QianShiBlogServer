using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using System.Security.Cryptography;
using System.Text;

namespace Domain.Helpers
{
    public static class PasswordHasher
    {
        public static (string Hashed, string Salt) HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(128 / 8);

            var hashed = Hash(password, salt);

            return (hashed, Convert.ToBase64String(salt));
        }

        private static string Hash(string value, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: value,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));
        }

        public static bool CompareHashPassword(string password, string saltStr, string hashedPassword)
        {
            var salt = Encoding.Default.GetBytes(saltStr);

            var hashed = Hash(password, salt);

            return hashed == hashedPassword;
        }
    }
}
