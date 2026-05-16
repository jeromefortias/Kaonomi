using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaonomi.Net.Core.Models.LLM
{
    /// <summary>
    /// Corps de requête chat générique (modèle, messages, mode flux).
    /// </summary>
    public class Request
    {
        /// <summary>Nom du modèle cible.</summary>
        public string? model { get; set; }
        /// <summary>Historique ou message utilisateur.</summary>
        public List<Message>? messages { get; set; }
        /// <summary>Si true, réponse en streaming (selon l'API appelée).</summary>
        public bool stream { get; set; } = false;
    }
}
