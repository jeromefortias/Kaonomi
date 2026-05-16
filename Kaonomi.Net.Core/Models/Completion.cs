namespace Kaonomi.Net.Core.Models
{
    /// <summary>
    /// Enregistrement d'une complétion LLM : prompt, réponse, modèle et métadonnées.
    /// </summary>
    public class Completion : EntityBase
    {
        /// <summary>Texte envoyé au modèle.</summary>
        public string prompt { get; set; } = string.Empty;
        /// <summary>Réponse générée par le modèle.</summary>
        public string completion { get; set; } = string.Empty;
        /// <summary>Identifiant du modèle utilisé.</summary>
        public string model { get; set; } = string.Empty;
        /// <summary>Métadonnées additionnelles (clé/valeur typées).</summary>
        public List<MetaData> metadatas { get; set; } = new List<MetaData>();
    }
}
