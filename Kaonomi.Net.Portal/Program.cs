namespace Kaonomi.Net.Portal
{
    /// <summary>
    /// Application Razor Pages : pages statiques, HTTPS, HSTS en production.
    /// </summary>
    public class Program
    {
        /// <summary>Configure les services Razor et le pipeline HTTP puis démarre l'écoute.</summary>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Ajout des services Razor Pages au conteneur DI.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Pipeline : exceptions, HSTS, fichiers statiques, autorisation, cartographie des pages.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // La valeur HSTS par défaut est 30 jours ; ajuster en production si besoin (https://aka.ms/aspnetcore-hsts).
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
