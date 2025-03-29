namespace torneo_calcetto_be.Core.Utility
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class PasswordHasher
    {
        // Genera l'hash di una password con PBKDF2
        public static string HashPassword(string password)
        {
            // Genera un salt casuale
            byte[] salt = GenerateSalt(16); // Lunghezza del salt (in byte)
                                            // Genera l'hash della password
            byte[] hash = HashWithPBKDF2(password, salt, 10000, 32); // Iterazioni e lunghezza hash

            // Combina salt e hash in un'unica stringa
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        // Verifica una password con un hash esistente
        public static bool VerifyPassword(string password, string storedHash)
        {
            // Divide salt e hash salvati
            var parts = storedHash.Split(':');
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedPasswordHash = Convert.FromBase64String(parts[1]);

            // Hash della password fornita dall'utente
            byte[] hash = HashWithPBKDF2(password, salt, 10000, storedPasswordHash.Length);

            // Confronta gli hash
            return CryptographicOperations.FixedTimeEquals(hash, storedPasswordHash);
        }

        // Genera un salt casuale
        private static byte[] GenerateSalt(int size)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[size];
                rng.GetBytes(salt);
                return salt;
            }
        }

        // Esegue l'hashing con PBKDF2
        private static byte[] HashWithPBKDF2(string password, byte[] salt, int iterations, int hashLength)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(hashLength);
            }
        }
    }

}
