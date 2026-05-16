using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kaonomi.Net.Core.Models.LLM
{
    /// <summary>
    /// Réponse brute de l'endpoint <c>/api/chat</c> d'Ollama (durées et compteurs d'évaluation).
    /// </summary>
    public class OllamaInternalResponse
    {
        /// <summary>Modèle ayant traité la requête.</summary>
        [JsonPropertyName("model")] public string Model { get; set; }
        /// <summary>Message assistant parsé.</summary>
        [JsonPropertyName("message")] public InternalMessage Message { get; set; }
        /// <summary>Durée totale en nanosecondes.</summary>
        [JsonPropertyName("total_duration")] public long TotalDuration { get; set; }
        /// <summary>Nombre de tokens évalués pour le prompt.</summary>
        [JsonPropertyName("prompt_eval_count")] public int PromptEvalCount { get; set; }
        /// <summary>Nombre de tokens générés en sortie.</summary>
        [JsonPropertyName("eval_count")] public int EvalCount { get; set; }
        /// <summary>Durée de l'évaluation de la sortie en nanosecondes.</summary>
        [JsonPropertyName("eval_duration")] public long EvalDuration { get; set; }
    }
}
