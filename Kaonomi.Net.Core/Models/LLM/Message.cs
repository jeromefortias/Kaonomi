using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaonomi.Net.Core.Models.LLM
{
    /// <summary>
    /// Message de conversation (rôle + contenu).
    /// </summary>
    public class Message
    {
        /// <summary>Rôle : system, user, assistant, etc.</summary>
        public string role { get; set; }
        /// <summary>Texte du message.</summary>
        public string content { get; set; } = string.Empty;
    }
}
