namespace Kaonomi.Net.Core
{
    using Models;
    using Kaonomi.Net.Core.Data;
    using System;

    /// <summary>
    /// Point d'accès singleton aux dépôts Elasticsearch (données, dialogue, admin) et persistance des logs / perf / complétions.
    /// </summary>
    public class SessionManager
    {
        private Config _config;
        /// <summary>Paramètres Elasticsearch du dernier dépôt initialisé (partagé entre branches).</summary>
        protected ElasticServerSettings ElasticSearchServerParameters;
        /// <summary>Dépôt « données » principal.</summary>
        protected ElasticRepository ElasticSearchServer;
        /// <summary>Dépôt dialogue (ou identique au serveur si non configuré).</summary>
        protected ElasticRepository ElasticSearchDialog;
        /// <summary>Dépôt administration / logs (ou identique au serveur si non configuré).</summary>
        protected ElasticRepository ElasticSearchAdmin;
        private static SessionManager instance;
        private static object locker = new object();

        /// <summary>Construit le gestionnaire et les clients Elasticsearch selon <see cref="Config"/>.</summary>
        public SessionManager(Config config)
        {
            _config = config;

            try
            {
                if (_config.dataRepository != null)
                {
                    ElasticSearchServerParameters = _config.dataRepository;
                    ElasticSearchServer = new ElasticRepository(ElasticSearchServerParameters);
                }

                if (_config.dialogRepository != null)
                {
                    ElasticSearchServerParameters = _config.dialogRepository;
                    ElasticSearchDialog = new ElasticRepository(ElasticSearchServerParameters);
                }
                else
                {
                    ElasticSearchDialog = ElasticSearchServer;
                }

                if (_config.adminRepository != null)
                {
                    ElasticSearchServerParameters = _config.adminRepository;
                    ElasticSearchAdmin = new ElasticRepository(ElasticSearchServerParameters);
                }
                else
                {
                    ElasticSearchAdmin = ElasticSearchServer;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        /// <summary>Retourne l'instance unique pour la configuration donnée (double-checked locking).</summary>
        public static SessionManager Get(Config config)
        {
            try
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new SessionManager(config);
                        }
                    }
                }
                return instance;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Enregistre un <see cref="Log"/> dans le dépôt admin et retourne l'id créé.</summary>
        public ServiceReturn LogSave(Log log)
        {
            ServiceReturn ret = new ServiceReturn();

            if (_config == null || ElasticSearchAdmin == null)
            {
                throw new Exception("Config not done");
            }

            try
            {
                string retid = "";
                retid = ElasticSearchAdmin.Save(log);
                ret.Message = retid + " id created. Log well saved";
                ret.ReturnedId = retid;
            }
            catch (Exception ex)
            {
                ret.Message = "Error : " + ex.Message;
                Console.WriteLine(ex.ToString());
                ret.ReturnedId = "-1";
            }

            return ret;
        }

        /// <summary>Enregistre une mesure de performance dans le dépôt admin.</summary>
        public ServiceReturn PerformanceSave(Performance perf)
        {
            ServiceReturn ret = new ServiceReturn();

            if (_config == null || ElasticSearchAdmin == null)
            {
                throw new Exception("Config not done");
            }

            try
            {
                string retid = "";
                retid = ElasticSearchAdmin.Save(perf);
                ret.Message = retid + " id created. Log well saved";
                ret.ReturnedId = retid;
            }
            catch (Exception ex)
            {
                ret.Message = "Error : " + ex.Message;
                Console.WriteLine(ex.ToString());
                ret.ReturnedId = "-1";
            }

            return ret;
        }

        /// <summary>Enregistre une complétion LLM dans le dépôt admin.</summary>
        public ServiceReturn CompletionSave(Completion completion)
        {
            ServiceReturn ret = new ServiceReturn();

            if (_config == null || ElasticSearchAdmin == null)
            {
                throw new Exception("Config not done");
            }

            try
            {
                string retid = "";
                retid = ElasticSearchAdmin.Save(completion);
                ret.Message = retid + " id created. Log well saved";
                ret.ReturnedId = retid;
            }
            catch (Exception ex)
            {
                ret.Message = "Error : " + ex.Message;
                Console.WriteLine(ex.ToString());
                ret.ReturnedId = "-1";
            }

            return ret;
        }



        /// <summary>Crée un log minimal à partir de champs courts et retourne l'id Elasticsearch.</summary>
        public string LogSave(string comment, string appName, string type)
        {
            Log log = new Log()
            {
                comment = comment,
                appName = appName,
                date = DateTime.Now,
                userName = Environment.UserName,
                machineName = Environment.MachineName,
                Type = type,
            };

            return ElasticSearchAdmin.Save<Log>(log);
        }

        /// <summary>Crée un <see cref="Log"/> détaillé puis délègue à <see cref="LogSave(Log)"/>.</summary>
        public ServiceReturn LogSave(string AppName, string MachineName, string UserName, string Message, string tags, string Type)
        {
            ServiceReturn ret = new ServiceReturn();

            if (_config == null || ElasticSearchAdmin == null)
            {
                throw new Exception("Config not done");
            }

            Log log = new Log();
            log.Type = Type;
            log.machineName = MachineName;
            log.userName = UserName;
            log.appName = AppName;
            log.date = DateTime.Now;
            log.comment = Message;
            ret = LogSave(log);
            return ret;
        }
    }
}
