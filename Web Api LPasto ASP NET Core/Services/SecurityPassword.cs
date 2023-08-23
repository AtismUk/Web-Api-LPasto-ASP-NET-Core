using System.Security.Cryptography;
using System.Text;

namespace Web_Api_LPasto_ASP_NET_Core.Services
{
    public static class SecurityPassword
    {
        public static byte[] SaltPassword()
        {
            byte[] salt = new byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                return salt;
            }
        }

        public static byte[] HashPassword(string password, byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            using var hash = new SHA256CryptoServiceProvider();

            return hash.ComputeHash(saltedPassword);
        }

        public static bool ComparePasswords(byte[] passwordDb, byte[] passwordUser)
        {
            bool res = passwordDb.SequenceEqual(passwordUser);
            return res;
        }

    }
}
