using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaonomi.Net.Core.Models.LLM
{
    /// <summary>
    /// Statistiques d'utilisation de tokens pour une requête/réponse.
    /// </summary>
    public class Usage
    {
        /// <summary>Tokens du prompt.</summary>
        public int prompt_tokens { get; set; }
        /// <summary>Tokens de la réponse.</summary>
        public int completion_tokens { get; set; }
        /// <summary>Total prompt + complétion.</summary>
        public int total_tokens { get; set; } = 0;
    }
}
