namespace Kaonomi.Net.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using Kaonomi.Net.Core.Models.LLM;

    /// <summary>
    /// Client HTTP minimal pour l'API chat d'Ollama (POST non streamé, agrégation des métriques).
    /// </summary>
    public static class HelperOllamaClient
    {
        /// <summary>Client réutilisé pour tous les appels (pool de connexions).</summary>
        private static readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Envoie une requête à Ollama et retourne le texte ainsi que les statistiques d'utilisation.
        /// </summary>
        public static async Task<OllamaChatResult> SendChatMessageAsync(string baseUrl, string modelName, string userPrompt)
        {
            var endpoint = $"{baseUrl.TrimEnd('/')}/api/chat";

            var requestBody = new
            {
                model = modelName,
                messages = new[]
                {
                    new { role = "user", content = userPrompt }
                },
                stream = false
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, requestBody);
                response.EnsureSuccessStatusCode();

                var rawResult = await response.Content.ReadFromJsonAsync<OllamaInternalResponse>();

                if (rawResult == null) return new OllamaChatResult { Content = "Erreur : Réponse vide." };

                // Calcul de la vitesse (Tokens par seconde)
                double tokensPerSecond = rawResult.EvalDuration > 0
                    ? (double)rawResult.EvalCount / (rawResult.EvalDuration / 1_000_000_000.0)
                    : 0;

                return new OllamaChatResult
                {
                    Content = rawResult.Message?.Content ?? "",
                    Model = rawResult.Model,
                    TotalTokens = rawResult.PromptEvalCount + rawResult.EvalCount,
                    PromptTokens = rawResult.PromptEvalCount,
                    ResponseTokens = rawResult.EvalCount,
                    TokensPerSecond = Math.Round(tokensPerSecond, 2),
                    TotalDurationSec = Math.Round(rawResult.TotalDuration / 1_000_000_000.0, 2)
                };
            }
            catch (Exception ex)
            {
                return new OllamaChatResult { Content = $"[Kaonomi Error] : {ex.Message}" };
            }
        }
    }

 



 
}