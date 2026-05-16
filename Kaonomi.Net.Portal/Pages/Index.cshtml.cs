using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaonomi.Net.Portal.Pages
{
    /// <summary>
    /// Page d'accueil du portail (logique serveur minimale).
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        /// <summary>Construit le modèle de page avec le journal applicatif.</summary>
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /// <summary>GET /Index : aucun état chargé pour l'instant.</summary>
        public void OnGet()
        {

        }
    }
}
