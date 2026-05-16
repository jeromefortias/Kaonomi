namespace Kaonomi.Net.Core.Helpers
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Utilitaires de hachage SHA-256 pour chaînes et fichiers.
    /// </summary>
    public class HelperSHA256
    {
        /// <summary>Calcule le hachage SHA-256 d'un fichier et le retourne en Base64.</summary>
        public static string FromFileGet(string filePath)
        {
            using (SHA256 SHA256 = SHA256Managed.Create())
            {
                using (FileStream fileStream = File.OpenRead(filePath))
                    return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
            }
        }
        /// <summary>Calcule le hachage SHA-256 d'une chaîne UTF-8 et le retourne en hexadécimal minuscule.</summary>
        public static string Get(string text)
        {
            try
            {
                SHA256 hash = SHA256.Create();
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder sha256signature = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sha256signature.Append(bytes[i].ToString("x2"));
                }
                return sha256signature.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
