namespace Kaonomi.Net.Server
{
    /// <summary>
    /// Réponse JSON simple contenant un texte concaténé.
    /// </summary>
    public class Result
    {
        /// <summary>Texte de résultat renvoyé au client.</summary>
        public string text { get; set; }
    }
}
