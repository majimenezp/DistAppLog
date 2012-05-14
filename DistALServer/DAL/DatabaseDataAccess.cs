// -----------------------------------------------------------------------
// <copyright file="DatabaseDataAccess.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DistALServer.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NHibernate;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;
    using NHibernate.Linq;
    using Entities;
    using NHibernate.Criterion;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DatabaseDataAccess:IDataAccess
    {

        ISessionFactory Sesion;
        Dictionary<string, long> appIds;
        public DatabaseDataAccess()
        {
            DistAppLogConfigurationSection config =
                       (DistAppLogConfigurationSection)System.Configuration.ConfigurationManager.GetSection("DistAppLogSection/Server");
            string connectionString = config.DataBase.SQLConnectionString;
            PersistenceProviders provider = config.DataBase.DatabaseProvider;
            Sesion = Fluently.Configure()
                .Database(CreateDbConfiguration(connectionString, provider))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Entities.Log>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
            appIds = new Dictionary<string, long>();
            FillAppIds();
        }

        private void FillAppIds()
        {
            using (var sesion = Sesion.OpenSession())
            {
                var appList = sesion.Query<Application>().ToList();
                foreach (var app in appList)
                {
                    appIds.Add(app.AppName.ToLower(), app.Id);
                }

            }
        }

        private IPersistenceConfigurer CreateDbConfiguration(string connectionString, PersistenceProviders provider)
        {
            int batchSize=100;
            IPersistenceConfigurer configuration;
            switch (provider)
            {
                case PersistenceProviders.SqlServer:
                    configuration = MsSqlConfiguration.MsSql2008.ConnectionString(connectionString).AdoNetBatchSize(batchSize);
                    break;
                case PersistenceProviders.Sqlite:
                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        configuration = SQLiteConfiguration.Standard.ConnectionString(connectionString).AdoNetBatchSize(batchSize);
                    }
                    else
                    {
                        configuration = SQLiteConfiguration.Standard.UsingFile(GetSqliteFilePath()).AdoNetBatchSize(batchSize);
                    }
                    break;
                default:                    
                    configuration = SQLiteConfiguration.Standard.UsingFile(GetSqliteFilePath()).AdoNetBatchSize(batchSize);
                    break;
            }
            return configuration;
        }

        private static string GetSqliteFilePath()
        {
            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var filePath = System.IO.Path.Combine(directory, "logdatabase.sqlite");
            return filePath;
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaUpdate(config).Execute(true, true);
        }

        private bool InsertLog(Log entity)
        {
            bool result = false;
            using (var sesion = Sesion.OpenStatelessSession())
            {
                using (var trans = sesion.BeginTransaction())
                {
                    try
                    {
                        sesion.Insert(entity);
                        trans.Commit();
                        result = true;
                    }
                    catch (Exception ex1)
                    {
                        trans.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public void InsertDebugMessage(DistALMessages.DebugMessage message)
        {
            long appId = CheckAppId(message.OriginIdentity);
            var tmp = new Log();
            tmp.Date = message.Date;
            tmp.App = message.OriginIdentity;
            tmp.AppIdentity = appId;
            tmp.Module = message.ModuleName;
            tmp.Level = "DEBUG";
            tmp.Message = message.Message;
            tmp.Exception = message.Stacktrace;
            bool inserted=InsertLog(tmp);
        }

        public void InsertErrorMessage(DistALMessages.ErrorMessage message)
        {
            long appId = CheckAppId(message.OriginIdentity);
            var tmp = new Log();
            tmp.Date = message.Date;
            tmp.AppIdentity = appId;
            tmp.App = message.OriginIdentity;
            tmp.Module = message.ModuleName;
            tmp.Level = "ERROR";
            tmp.Message = message.Message;
            tmp.Exception = message.Exception.ToString();
            bool inserted = InsertLog(tmp);
        }

        public void InsertFatalMessage(DistALMessages.FatalErrorMessage message)
        {
            long appId = CheckAppId(message.OriginIdentity);
            if (message != null)
            {
                var tmp = new Log();
                tmp.Date = message.Date == DateTime.MinValue ? DateTime.Now : message.Date;
                tmp.AppIdentity = appId;
                tmp.App = message.OriginIdentity;
                tmp.Module = message.ModuleName ?? string.Empty;
                tmp.Level = "FATAL";
                tmp.Message = message.Message??string.Empty;
                tmp.Exception = message.Exception==null?string.Empty:message.Exception.ToString();
                bool inserted = InsertLog(tmp);
            }
        }

        public void InsertHitMessage(DistALMessages.HitMessage message)
        {
            long appId = CheckAppId(message.OriginIdentity);
            var tmp = new Log();
            tmp.Date = message.DateofHit;
            tmp.AppIdentity = appId;
            tmp.App = message.OriginIdentity;
            tmp.Module = message.ModuleName;
            tmp.Level = "HIT";
            tmp.Message = "Hit by user:" + message.User + "|Message:" + message.Message;
            tmp.Exception = string.Empty;
            bool inserted = InsertLog(tmp);
        }

        public void InsertInfoMessage(DistALMessages.InfoMessage message)
        {
            long appId = CheckAppId(message.OriginIdentity);
            var tmp = new Log();
            tmp.Date = DateTime.Now;
            tmp.App = message.OriginIdentity;
            tmp.AppIdentity = appId;
            tmp.Module = message.ModuleName;
            tmp.Level = "INFO";
            tmp.Message = message.Message;
            tmp.Exception = string.Empty;
            bool inserted = InsertLog(tmp);
        }

        public void InsertWarningMessage(DistALMessages.WarningMessage message)
        {
            long appId = CheckAppId(message.OriginIdentity);
            var tmp = new Log();
            tmp.Date = message.Date== DateTime.MinValue?DateTime.Now:message.Date;
            tmp.AppIdentity = appId;
            tmp.App = message.OriginIdentity;
            tmp.Module = message.ModuleName ?? string.Empty ;
            tmp.Level = "WARNING";
            tmp.Message = message.Message ?? string.Empty;
            tmp.Exception = message.Exception == null ? string.Empty : message.Exception.ToString();
            bool inserted = InsertLog(tmp);
        }

        public Entities.Log[] GetLog(int PageNumber, int ItemsPerPage, out int TotalPages)
        {
            using (var sesion = Sesion.OpenSession())
            {
                var results = sesion.Query<Log>().Skip((PageNumber - 1) * ItemsPerPage).Take(ItemsPerPage).ToArray();
                var totalItems = sesion.Query<Log>().Count();
                TotalPages = totalItems / ItemsPerPage + ((totalItems % ItemsPerPage) > 0 ? 1 : 0);
                return results;
            }
        }

        public Entities.Log[] GetLog()
        {
            Log[] logs = null;
            using (var sesion = Sesion.OpenSession())
            {
                logs = sesion.Query<Log>().OrderByDescending(x => x.Id).ToArray();
            }
            return logs;
        }

        public long CheckAppId(string appname)
        {
            Application app = null;
            if (appIds.ContainsKey(appname.ToLower()))
            {
                return appIds[appname.ToLower()];
            }
            else
            {
                using (var sesion = Sesion.OpenSession())
                {
                    var reg = sesion.CreateCriteria<Application>().Add(Restrictions.InsensitiveLike("AppName", appname)).List<Application>();
                    app = reg.FirstOrDefault();
                    if (app == null)
                    {
                        using (var trans = sesion.BeginTransaction())
                        {
                            app = new Application();
                            app.AppName = appname;
                            sesion.SaveOrUpdate(app);
                            trans.Commit();
                        }
                    }
                    appIds.Add(app.AppName.ToLower(), app.Id);
                }
            }
            return app.Id;
        }
    }
}
