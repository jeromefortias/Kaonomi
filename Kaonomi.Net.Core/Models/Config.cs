namespace Kaonomi.Net.Core.Models
{
    /// <summary>
    /// Configuration globale de l'application : identité, LLM, chemins et connexions Elasticsearch.
    /// </summary>
    public class Config

    {
        /// <summary>Nom de l'application.</summary>
        public string appName { get; set; } = "APPx";
        /// <summary>Projet ou regroupement logique (ex. « common »).</summary>
        public string? projectName { get; set; } = "common";
        /// <summary>Langue ou locale cible (« * » pour toutes).</summary>
        public string? language { get; set; } = "*";
        /// <summary>Catégorie fonctionnelle optionnelle.</summary>
        public string? category { get; set; }
        /// <summary>Description courte de l'application.</summary>
        public string appDescription { get; set; } = "Description of the application ";
        /// <summary>URL de la documentation associée.</summary>
        public string appDocumentationUrl { get; set; } = @"https://www.kaonomi.net";
        /// <summary>URL de base du serveur Ollama (ex. http://localhost:11434).</summary>
        public string ollamaUrl { get; set; } = "http://localhost:11434";
        /// <summary>URL optionnelle du serveur de dialogue.</summary>
        public string? dialogServerUrl { get; set; }
        /// <summary>Racine des documents sur disque.</summary>
        public string? documentsRootFolder { get; set; } = "C:\\Documents";
        /// <summary>URL optionnelle d'un robot ou service externe.</summary>
        public string? robotUrl { get; set; }
        /// <summary>Paramètres Elasticsearch pour les données principales.</summary>
        public ElasticServerSettings? dataRepository { get; set; }
        /// <summary>Paramètres Elasticsearch pour le dialogue (sinon réutilisation du dépôt principal).</summary>
        public ElasticServerSettings? dialogRepository { get; set; }
        /// <summary>Paramètres Elasticsearch pour l'administration / logs (sinon réutilisation du dépôt principal).</summary>
        public ElasticServerSettings? adminRepository { get; set; }
        /// <summary>Nom du modèle LLM par défaut (Ollama).</summary>
        public string llmmodel { get; set; } = "gemma3:4b";
        /// <summary>Latence cible ou seuil en millisecondes.</summary>
        public int LatencyMs { get; set; } = 100;
    }
}
