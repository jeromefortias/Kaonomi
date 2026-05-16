namespace Kaonomi.Net.Core.Models
{
    /// <summary>
    /// Paramètres de connexion à un cluster Elasticsearch (URL, index, authentification).
    /// </summary>
    public class ElasticServerSettings
    {
        /// <summary>URL du nœud ou du cluster.</summary>
        public string? url { get; set; } = "http://localhost:9200";
        /// <summary>Couche ou environnement logique.</summary>
        public string? layer { get; set; } = "Default";
        /// <summary>Préfixe des noms d'index.</summary>
        public string? prefix { get; set; } = "";
        /// <summary>Mot de passe pour l'authentification basique (avec user).</summary>
        public string? password { get; set; }
        /// <summary>Nom d'utilisateur pour l'authentification basique.</summary>
        public string? user { get; set; }
    }
}
