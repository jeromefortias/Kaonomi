

namespace Kaonomi.Net.Core.Data
{
    using Models;
    using Nest;

    /// <summary>
    /// Implémentation Nest des opérations CRUD Elasticsearch pour les entités Kaonomi.
    /// </summary>
    public class ElasticRepository : iElasticRepository<Log>
    {
        protected ElasticServerSettings _ESconfig;
        /// <summary>Crée un client configuré à partir des paramètres serveur.</summary>
        public ElasticRepository(ElasticServerSettings ESconfig)
        {
            _ESconfig = ESconfig;
        }

        /// <summary>Enregistre plusieurs entités et retourne leurs identifiants.</summary>
        public IEnumerable<string> Save<TEntity>(params TEntity[] entities)
            where TEntity : EntityBase
        {
            foreach (var entity in entities)
                yield return SaveImpl(entity);
        }

        /// <summary>Enregistre une énumération d'entités et retourne leurs identifiants.</summary>
        public IEnumerable<string> Save<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : EntityBase
        {
            foreach (var entity in entities)
                yield return SaveImpl(entity);
        }

        /// <summary>Indexe une entité et retourne l'id du document.</summary>
        public string Save<TEntity>(TEntity entity)
            where TEntity : EntityBase
        {
            return SaveImpl(entity);
        }

        /// <summary>Récupère une entité par identifiant.</summary>
        public Object Load<TEntity>(string id)
            where TEntity : EntityBase
        {
            return LoadImpl<TEntity>(id);
        }

        /// <summary>Recherche floue limitée sur le champ <c>comment</c> (10 résultats).</summary>
        public IReadOnlyCollection<TEntity> Search<TEntity>(string freeText)
            where TEntity : EntityBase
        {
            return SearchImpl<TEntity>(freeText);
        }



        /// <summary>Enregistre un <see cref="Log"/> (implémentation explicite du contrat log).</summary>
        public string Save(Log entity)
        {
            return SaveImpl(entity);
        }

        /// <summary>Supprime un document par id dans l'index du type <typeparamref name="TEntity"/>.</summary>
        public bool DeleteImpl<TEntity>(string id) where TEntity : EntityBase
        {
            bool res;
            try
            {
                var uri = new Uri(_ESconfig.url);
                bool hasPassword = false;
                // check as a password 
                if (_ESconfig.password != null && _ESconfig.user != null)
                {
                    if (_ESconfig.password.Length > 0 && _ESconfig.user.Length > 0)
                    {
                        hasPassword = true;
                    }
                }
                var settings = new ConnectionSettings();
                if (hasPassword)
                {
                    settings = new ConnectionSettings(uri).BasicAuthentication(_ESconfig.user, _ESconfig.password)
                    .DefaultIndex(_ESconfig.prefix + typeof(TEntity).Name.ToLower());
                }
                else
                {
                    settings = new ConnectionSettings(new Uri(_ESconfig.url)).DefaultIndex(_ESconfig.prefix + typeof(TEntity).Name.ToLower());
                }
                var client = new ElasticClient(settings);

                var response = client.Delete<TEntity>(id);
                res = true;

            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        /// <summary>Charge un document par id via <see cref="ElasticClient.Get{TDocument}"/>.</summary>
        private Object LoadImpl<TEntity>(string id) where TEntity : EntityBase
        {
            Object entity;
            try
            {
                var uri = new Uri(_ESconfig.url);
                bool hasPassword = false;
                // check as a password 
                if (_ESconfig.password != null && _ESconfig.user != null)
                {
                    if (_ESconfig.password.Length > 0 && _ESconfig.user.Length > 0)
                    {
                        hasPassword = true;
                    }
                }
                var settings = new ConnectionSettings();
                if (hasPassword)
                {
                    settings = new ConnectionSettings(uri).BasicAuthentication(_ESconfig.user, _ESconfig.password)
                    .DefaultIndex(_ESconfig.prefix + typeof(TEntity).Name.ToLower());
                }
                else
                {
                    settings = new ConnectionSettings(new Uri(_ESconfig.url)).DefaultIndex(_ESconfig.prefix + typeof(TEntity).Name.ToLower());
                }
                var client = new ElasticClient(settings);

                var response = client.Get<TEntity>(id, g => g.Index(typeof(TEntity).Name.ToLower()));
                entity = response.Source;
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }


        /// <summary>Exécute une requête fuzzy sur <c>comment</c> (taille 10).</summary>
        private IReadOnlyCollection<TEntity> SearchImpl<TEntity>(string freeText) where TEntity : EntityBase
        {
            IReadOnlyCollection<TEntity> entity;
            try
            {
                var uri = new Uri(_ESconfig.url);
                bool hasPassword = false;
                // check as a password 
                if (_ESconfig.password != null && _ESconfig.user != null)
                {
                    if (_ESconfig.password.Length > 0 && _ESconfig.user.Length > 0)
                    {
                        hasPassword = true;
                    }
                }
                var settings = new ConnectionSettings();
                if (hasPassword)
                {
                    settings = new ConnectionSettings(uri).BasicAuthentication(_ESconfig.user, _ESconfig.password)
                    .DefaultIndex(_ESconfig.prefix + typeof(TEntity).Name.ToLower());
                }
                else
                {
                    settings = new ConnectionSettings(new Uri(_ESconfig.url)).DefaultIndex(_ESconfig.prefix + typeof(TEntity).Name.ToLower());
                }
                var client = new ElasticClient(settings);
                //TODO - Extend the search method
                var searchResponse3 = client.Search<TEntity>(s => s
                                                                .From(0)
                                                                .Size(10)
                                                                .Query(q => q
                                                                        .Fuzzy(c => c
                                                                            .Name("fuzzy")
                                                                            .Boost(1.1)
                                                                            .Field(p => p.comment)
                                                                            .Fuzziness(Fuzziness.Auto)
                                                                            .Value(freeText)
                                                                            .MaxExpansions(100)
                                                                              )
                                                                      )
                                                            );
                entity = searchResponse3.Documents;
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }
        /// <summary>Indexe le document et renvoie l'id retourné par Elasticsearch (chaîne vide si absent).</summary>
        private string SaveImpl<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            string res = "";
            var uri = new Uri(_ESconfig.url);
            bool hasPassword = false;
            // check as a password 
            if (_ESconfig.password != null && _ESconfig.user != null)
            {
                if (_ESconfig.password.Length > 0 && _ESconfig.user.Length > 0)
                {
                    hasPassword = true;
                }
            }
            var settings = new ConnectionSettings();
            if (hasPassword)
            {
                settings = new ConnectionSettings(uri).BasicAuthentication(_ESconfig.user, _ESconfig.password)
                .DefaultIndex(_ESconfig.prefix + typeof(TEntity).Name.ToLower());
            }
            else
            {
                settings = new ConnectionSettings(new Uri(_ESconfig.url)).DefaultIndex(_ESconfig.prefix + typeof(TEntity).Name.ToLower());
            }
            var client = new ElasticClient(settings);
            // return un null et si on ajoute ?? cela renvoie un Empty
            res = client.IndexDocument(entity)?.Id ?? string.Empty;
            return res;
        }

        /// <summary>Ré-indexe un document (même logique que l'insertion pour Nest).</summary>
        private string UpdateImpl<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            string res = "";
            var uri = new Uri(_ESconfig.url);
            bool hasPassword = false;
            // check as a password 
            if (_ESconfig.password != null && _ESconfig.user != null)
            {
                if (_ESconfig.password.Length > 0 && _ESconfig.user.Length > 0)
                {
                    hasPassword = true;
                }
            }
            var settings = new ConnectionSettings();
            if (hasPassword)
            {
                settings = new ConnectionSettings(uri).BasicAuthentication(_ESconfig.user, _ESconfig.password)
                .DefaultIndex(_ESconfig.prefix + typeof(TEntity).Name.ToLower());
            }
            else
            {
                settings = new ConnectionSettings(new Uri(_ESconfig.url)).DefaultIndex(_ESconfig.prefix + typeof(TEntity).Name.ToLower());
            }
            var client = new ElasticClient(settings);

            // return un null et si on ajoute ?? cela renvoie un Empty
            res = client.IndexDocument(entity)?.Id ?? string.Empty;
            return res;
        }


    }
}
