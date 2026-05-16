using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaonomi.Net.Core.Models.LLM
{
    /// <summary>
    /// Un choix de complétion : index, message assistant, raison de fin.
    /// </summary>
    public class Choice
    {
        /// <summary>Index du choix dans la liste.</summary>
        public int index { get; set; }
        /// <summary>Message généré (souvent rôle assistant).</summary>
        public Message message { get; set; }
        /// <summary>Raison d'arrêt (stop, length, etc.).</summary>
        public string finish_reason { get; set; } = string.Empty;
    }
}
