using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Backend.Domain
{
    public class PasswordHasher
    {
        private const int SaltSize = 16; 
        private const int KeySize = 32; 
        private const int Iterations = 100000; 

        public static string HashPassword(string password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt); 
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    byte[] key = pbkdf2.GetBytes(KeySize);
                    byte[] hashBytes = new byte[SaltSize + KeySize];
                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(key, 0, hashBytes, SaltSize, KeySize);

                    return $"PBKDF2${Convert.ToBase64String(hashBytes)}";
                }
            }
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            if (!storedHash.StartsWith("PBKDF2$"))
                return false;

            string hashBase64 = storedHash.Split('$')[1];
            byte[] hashBytes = Convert.FromBase64String(hashBase64);

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] key = pbkdf2.GetBytes(KeySize);

                for (int i = 0; i < KeySize; i++)
                {
                    if (hashBytes[i + SaltSize] != key[i])
                        return false;
                }
            }
            return true;
        }

    }
}
