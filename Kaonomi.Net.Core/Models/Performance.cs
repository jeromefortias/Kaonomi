namespace Kaonomi.Net.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Mesure de performance d'une opération (durées, nom d'opération, métadonnées).
    /// </summary>
    public class Performance : EntityBase
    {
        /// <summary>Nom logique de l'opération mesurée.</summary>
        public string operationName { get; set; } = string.Empty;
        /// <summary>Heure de début.</summary>
        public DateTime startTime { get; set; }
        /// <summary>Heure de fin.</summary>
        public DateTime endTime { get; set; }
        /// <summary>Durée totale en millisecondes.</summary>
        public int durationMilliseconds { get; set; }
        /// <summary>Détails contextuels (tailles, modèle, etc.).</summary>
        public List<MetaData> metadatas { get; set; } = new List<MetaData>();

    }
}
