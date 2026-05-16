

namespace Kaonomi.Net.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Charge des objets de configuration depuis un fichier JSON.
    /// </summary>
    public static class HelperConfigManager
    {
        /// <summary>Lit le fichier JSON et désérialise en <typeparamref name="T"/> ; crée une instance vide si le JSON est null.</summary>
        /// <param name="fullName">Chemin complet du fichier.</param>
        public static T? GetFromFile<T>(string fullName) where T : new()
        {
            try
            {
                string json = File.ReadAllText(fullName);

                return JsonSerializer.Deserialize<T>(json) ?? new T();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
