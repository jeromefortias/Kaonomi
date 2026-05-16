using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaonomi.Net.Portal.Pages
{
    /// <summary>
    /// Page « Confidentialité » (gabarit Razor par défaut).
    /// </summary>
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>Construit le modèle de page confidentialité.</summary>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>GET /Privacy.</summary>
        public void OnGet()
        {
        }
    }

}
