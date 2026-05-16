using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaonomi.Net.Core.Models.LLM
{
    /// <summary>
    /// Réponse type « chat completion » (identifiants, choix, usage tokens).
    /// </summary>
    public class Response
    {
        /// <summary>Identifiant de la complétion.</summary>
        public string? id { get; set; }
        /// <summary>Type d'objet retourné par l'API.</summary>
        public string? @object { get; set; } = "chat.completion";
        /// <summary>Horodatage de création (epoch ou selon fournisseur).</summary>
        public int? created { get; set; }
        /// <summary>Modèle ayant produit la réponse.</summary>
        public string? model { get; set; } = string.Empty;
        /// <summary>Empreinte système optionnelle.</summary>
        public string? system_fingerprint { get; set; }
        /// <summary>Alternatives de réponse (souvent une seule entrée).</summary>
        public List<Choice>? choices { get; set; } = new List<Choice>();
        /// <summary>Comptage des tokens consommés.</summary>
        public Usage? usage { get; set; } = new Usage();
    }
}
