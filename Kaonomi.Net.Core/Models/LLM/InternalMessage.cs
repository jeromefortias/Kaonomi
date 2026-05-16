using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kaonomi.Net.Core.Models.LLM
{
    /// <summary>
    /// Fragment de message tel que renvoyé par l'API Ollama (désérialisation JSON).
    /// </summary>
    public class InternalMessage
    {
        /// <summary>Contenu textuel du message.</summary>
        [JsonPropertyName("content")] public string Content { get; set; }
    }
}
