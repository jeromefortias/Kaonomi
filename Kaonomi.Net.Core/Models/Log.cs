using System;
namespace Kaonomi.Net.Core.Models
{
    /// <summary>
    /// Entrée de journal applicatif avec niveau de gravité contrôlé (INFO, ERROR, etc.).
    /// </summary>
    public class Log : EntityBase
    {
        private string _Type = "";
        /// <summary>Crée un log avec identifiant généré.</summary>
        public Log() : base()
        {
            Init();
        }
        /// <summary>Crée un log avec l'identifiant indiqué.</summary>
        public Log(string? id) : base(id)
        {
            Init();
        }

        /// <summary>
        /// Type de log : normalisé en majuscules ; valeurs autorisées INFO, ERROR, WARNING, ALERT, OK, DONE, PERF (sinon INFO).
        /// </summary>
        public string Type
        {
            get { return _Type; }
            set
            {
                value = value.ToUpper().Trim();
                if (!(value == "INFO" || value == "ERROR" || value == "WARNING" || value == "ALERT" || value == "OK" || value == "DONE" || value == "PERF"))
                {
                    _Type = "INFO";
                }
                else
                {
                    _Type = value;
                }
            }
        }

        private void Init()
        {

        }
    }
}
