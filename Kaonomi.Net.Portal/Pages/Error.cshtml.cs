using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Kaonomi.Net.Portal.Pages
{
    /// <summary>
    /// Page d'erreur : expose un RequestId pour corréler les journaux (pas de cache).
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        /// <summary>Identifiant de requête ou de trace pour le support.</summary>
        public string? RequestId { get; set; }

        /// <summary>True si un RequestId est disponible à l'affichage.</summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        /// <summary>Construit le modèle de page erreur.</summary>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>Renseigne <see cref="RequestId"/> à partir de l'activité courante ou du trace identifier HTTP.</summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }

}
