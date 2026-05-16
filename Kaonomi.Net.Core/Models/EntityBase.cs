namespace Kaonomi.Net.Core.Models
{
    /// <summary>
    /// Entité de base indexable : identifiant, contexte machine/utilisateur et commentaire.
    /// </summary>
    public class EntityBase
    {
        /// <summary>Construit une entité avec un nouvel identifiant GUID.</summary>
        public EntityBase()
        {
            this.id = Guid.NewGuid().ToString();
        }
        /// <summary>Construit une entité avec l'identifiant fourni ou un GUID si null.</summary>
        /// <param name="id">Identifiant existant ou null pour en générer un.</param>
        public EntityBase(string? id)
        {
            if (id == null)
            {
                this.id = Guid.NewGuid().ToString();
            }
            else
            {
                this.id = id;
            }
            Init();
        }
        /// <summary>Nom de l'application propriétaire de l'enregistrement.</summary>
        public string appName { get; set; }
        /// <summary>Identifiant unique du document (Elasticsearch / stockage).</summary>
        public string id { get; set; }
        /// <summary>Horodatage associé à l'événement ou à la création.</summary>
        public DateTime date { get; set; }
        /// <summary>Nom de la machine d'origine.</summary>
        public string machineName { get; set; }
        /// <summary>Nom de l'utilisateur d'origine.</summary>
        public string userName { get; set; }
        /// <summary>Texte libre ou libellé descriptif.</summary>
        public string comment { get; set; }
        /// <summary>Point d'extension pour une initialisation dérivée.</summary>
        private void Init()
        {

        }
    }
}
