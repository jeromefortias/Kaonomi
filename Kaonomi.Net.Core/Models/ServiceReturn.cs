namespace Kaonomi.Net.Core.Models
{
    /// <summary>
    /// Enveloppe de retour pour les opérations de persistance (message, id créé, succès).
    /// </summary>
    public class ServiceReturn
    {
        /// <summary>Message utilisateur ou technique.</summary>
        public string? Message { get; set; }
        /// <summary>Identifiant retourné par le stockage (ou « -1 » en cas d'erreur).</summary>
        public string? ReturnedId { get; set; }
        /// <summary>Indique si l'opération est considérée comme réussie.</summary>
        public bool Success { get; set; } = true;
    }
}
