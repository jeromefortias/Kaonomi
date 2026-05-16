namespace Kaonomi.Net.Core.Models.LLM
{
    /// <summary>
    /// Résultat normalisé d'un appel chat Ollama : texte, modèle et métriques de performance.
    /// </summary>
    public class OllamaChatResult
    {
        /// <summary>Contenu de la réponse affichable.</summary>
        public string Content { get; set; }
        /// <summary>Modèle utilisé.</summary>
        public string Model { get; set; }
        /// <summary>Total de tokens (prompt + réponse).</summary>
        public int TotalTokens { get; set; }
        /// <summary>Tokens côté prompt.</summary>
        public int PromptTokens { get; set; }
        /// <summary>Tokens côté réponse.</summary>
        public int ResponseTokens { get; set; }
        /// <summary>Débit approximatif en tokens par seconde.</summary>
        public double TokensPerSecond { get; set; }
        /// <summary>Durée totale du traitement en secondes.</summary>
        public double TotalDurationSec { get; set; }
    }
}
