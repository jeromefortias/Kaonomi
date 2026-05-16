using Microsoft.AspNetCore.Http;

namespace Kaonomi.Net.Server
{
    /// <summary>
    /// Hôte API de démo : Swagger + routes GET/POST <c>root</c> pour concaténer un nom.
    /// </summary>
    internal class Program
    {
        /// <summary>Point d'entrée : pipeline minimal ASP.NET Core avec OpenAPI.</summary>
        static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddSwaggerGen(s => s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Tuto Demo", Version = "v1" })).AddEndpointsApiExplorer();

            var app = builder.Build();

            app.UseSwagger();

            // POST : corps JSON { firstName, lastName } → Result.text
            app.MapPost("root", (Person p) =>
            {
                string res = p.FirstName + " " + p.LastName;

                Result result = new Result();
                result.text = res;

                return Task.FromResult(result);
            });

            // GET : paramètres query firstname, lastname → chaîne concaténée
            app.MapGet("root", (string firstname, string lastname) =>
            {
                string res = firstname + " " + lastname;
                return Task.FromResult(res);
            }

            );

            // Racine : redirection vers l'UI Swagger
            app.MapGet("/", (request) =>
            {
                request.Response.Redirect("swagger");

                return Task.CompletedTask;
            });

            app.UseSwaggerUI();
            app.Run();




        }

    }
}
