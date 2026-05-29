using Kaonomi.Net.Core;
using Kaonomi.Net.Core.Helpers;
using Kaonomi.Net.Core.Models;

namespace Kaonomi.Net.Admin
{
    /// <summary>
    /// Console d'administration : charge la config, journalise dans Elasticsearch, boucle REPL vers Ollama.
    /// J'ajoute ce commentaire pour tester GIT
    /// superrrqsqqsq
    /// </summary>
    internal class Program
    {
        static private SessionManager _session;
        static private Config _config;
        static private string _defaultModel;

        /// <summary>Point d'entrée : configuration, session, boucle de saisie et appels LLM asynchrones.</summary>
        /// <param name="args">Arguments ligne de commande (non utilisés).</param>
        static async Task Main(string[] args)
        {
            string ollamaUrl = "http://localhost:11434";
            string defaultModel = "mistral-small3.2";
            _defaultModel = defaultModel;

            // Chargement de la config
            Config config = HelperConfigManager.GetFromFile<Config>("config.json");
            _config = config;
            string AppName = config.appName;

            Console.WriteLine($"Starting {config.appName} - {config.appDescription}");
            Console.WriteLine($"Documentation: {config.appDocumentationUrl}");
            Console.WriteLine("https://www.kaonomi.net");

            _session = new SessionManager(_config);
            _session.LogSave("Application started", AppName, "Info");

            string input = "";
            string path = "root/";

            while (true)
            {
                // Gestion du Prompt UI
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("kaonomi.net@");
                Console.Write(Environment.UserName);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(":");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(path);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("$ ");
                Console.ForegroundColor = ConsoleColor.Cyan;

                input = Console.ReadLine();

                // Conditions de sortie
                if (string.IsNullOrEmpty(input) || input == "/bye" || input == "quit()" || input == "/exit" || input == "/quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (string.IsNullOrEmpty(input) || input == "/model" || input == "/Model" )
                {
                    Console.WriteLine("Selected model : "+_defaultModel);
                    
                }
                else
                {
                    await OllamaCall(ollamaUrl, defaultModel, AppName, input);

                }
                Console.WriteLine(); // Saut de ligne pour la lisibilité
            }

            _session.LogSave("Application stopped", AppName, "Info");
        }

        /// <summary>Envoie le prompt à Ollama, affiche la réponse, persiste logs / perf / complétion.</summary>
        private static async Task OllamaCall(string ollamaUrl, string defaultModel, string AppName, string input)
        {
            try
            {
                Performance perf = new Performance();
                perf.date = DateTime.Now;
                perf.comment = "LLM Call Performance using " + _defaultModel + ".";
                perf.machineName = Environment.MachineName;
                perf.userName = Environment.UserName;
                perf.appName = _config.appName;
                perf.operationName = "LLM Call";
                perf.startTime = perf.date;
                perf.metadatas = new List<MetaData>();
                MetaData nxMetaData = new MetaData();
                nxMetaData.Key = "Model";
                nxMetaData.Value_String = defaultModel;
                perf.metadatas.Add(nxMetaData);
                MetaData nxMetaData2 = new MetaData();
                nxMetaData2.Key = "InputLength";
                nxMetaData2.Value_Int = input.Length;
                perf.metadatas.Add(nxMetaData2);

                var result = await HelperOllamaClient.SendChatMessageAsync(ollamaUrl, defaultModel, input);

                // Affichage des résultats
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nIA : {result.Content}");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[Stats: {result.ResponseTokens} tokens | {result.TokensPerSecond} tk/s | {result.TotalDurationSec}s]");
                Console.ResetColor();

                // Sauvegarde du log avec le vrai contenu de la réponse
                _session.LogSave($"User input: {input} | Response: {result.Content}", AppName, "Info");

                MetaData nxMetaData3 = new MetaData();
                nxMetaData3.Key = "OutputLength";
                nxMetaData3.Value_Int = result.Content.Length;
                perf.metadatas.Add(nxMetaData3);

                MetaData nxMetaData4 = new MetaData();
                nxMetaData4.Key = "ResponseTokens";
                nxMetaData4.Value_Int = result.ResponseTokens;
                perf.metadatas.Add(nxMetaData4);

                MetaData nxMetaData5 = new MetaData();
                nxMetaData5.Key = "LmmProcessCompletionInMs";
                nxMetaData5.Value_Double = result.TotalDurationSec;
                perf.metadatas.Add(nxMetaData5);


                perf.endTime = DateTime.Now;
                perf.durationMilliseconds = (int)(perf.endTime - perf.startTime).TotalMilliseconds;
                _session.PerformanceSave(perf);

                Completion nxCompletion = new Completion();
                nxCompletion.prompt = input;
                nxCompletion.completion = result.Content;
                nxCompletion.model = defaultModel;
                nxCompletion.metadatas = new List<MetaData>();
                nxCompletion.date = DateTime.Now;
                nxCompletion.userName = Environment.UserName;
                nxCompletion.machineName = Environment.MachineName;
                nxCompletion.appName = _config.appName;
                nxCompletion.comment = "LLM Completion using " + defaultModel + ".";
                _session.CompletionSave(nxCompletion);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[Error]: {ex.Message}");
                Console.ResetColor();
                _session.LogSave($"Error during Ollama call: {ex.Message}", AppName, "Error");
            }
        }
    }
}