using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaonomi.Net.Core.Models
{
    /// <summary>
    /// Paire clé / valeur typée pour enrichir logs, performances ou complétions.
    /// </summary>
    public class MetaData
    {
        /// <summary>Nom de la métadonnée.</summary>
        public string Key { get; set; } = string.Empty;
        /// <summary>Valeur textuelle.</summary>
        public string Value_String { get; set; } = string.Empty;
        /// <summary>Valeur entière.</summary>
        public int Value_Int { get; set; }
        /// <summary>Valeur flottante.</summary>
        public double Value_Double { get; set; }

    }
}
