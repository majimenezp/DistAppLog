using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;
using DistALServer.DAL.Entities;

namespace DistALServer.DAL
{
    public class RedisDataAccess:IDataAccess
    {
        readonly RedisClient client;
        public RedisDataAccess()
        {
            DistAppLogConfigurationSection config =
                      (DistAppLogConfigurationSection)System.Configuration.ConfigurationManager.GetSection("DistAppLogSection/Server");
            client = new RedisClient(config.DataBase.RedisServer);
            client.Db = config.DataBase.RedisDbNumber;
        }
        public void InsertDebugMessage(DistALMessages.DebugMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity);
            var tmp = new Log();
            tmp.Date = message.Date;
            tmp.AppIdentity = appid;
            tmp.Module = message.ModuleName;
            tmp.Message = message.Message;
            tmp.Exception = message.Stacktrace;
            tmp.Level = "DEBUG";
            using (var log = client.GetTypedClient<Log>())
            {
                tmp.Id = log.GetNextSequence();
                using (var trans = client.CreateTransaction())
                {
                    trans.QueueCommand(x => x.Store(tmp));
                    trans.Commit();
                }
            } 
        }

        public void InsertErrorMessage(DistALMessages.ErrorMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity);
            var tmp = new Log();
            tmp.Date = message.Date;
            tmp.AppIdentity = appid;
            tmp.Module = message.ModuleName;
            tmp.Message = message.Message;
            tmp.Exception = message.Exception.ToString();
            tmp.Level = "ERROR";
            using (var log = client.GetTypedClient<Log>())
            {
                tmp.Id = log.GetNextSequence();
                using (var trans = client.CreateTransaction())
                {
                    trans.QueueCommand(x => x.Store(tmp));
                    trans.Commit();
                }
            } 
        }

        public void InsertFatalMessage(DistALMessages.FatalErrorMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity);
            var tmp = new Log();
            tmp.Date = message.Date;
            tmp.AppIdentity = appid;
            tmp.Module = message.ModuleName;
            tmp.Message = message.Message ?? string.Empty;
            tmp.Exception = message.Exception??string.Empty;
            tmp.Level = "FATAL";
            using (var log = client.GetTypedClient<Log>())
            {
                tmp.Id = log.GetNextSequence();
                using (var trans = client.CreateTransaction())
                {
                    trans.QueueCommand(x => x.Store(tmp));
                    trans.Commit();
                }
            } 
        }

        public void InsertHitMessage(DistALMessages.HitMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity);
            var tmp = new Log();
            tmp.Date = message.DateofHit;
            tmp.AppIdentity = appid;
            tmp.Module = message.ModuleName;
            tmp.Message = "Hit by user:" + message.User + "|Message:" + message.Message;
            tmp.Exception = string.Empty;
            tmp.Level = "HIT";
            using (var log = client.GetTypedClient<Log>())
            {
                tmp.Id = log.GetNextSequence();
                using (var trans = client.CreateTransaction())
                {
                    trans.QueueCommand(x => x.Store(tmp));
                    trans.Commit();
                }
            }
        }

        public void InsertInfoMessage(DistALMessages.InfoMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity);
            var tmp = new Log();
            tmp.Date = DateTime.Now;
            tmp.AppIdentity = appid;
            tmp.Module = message.ModuleName;
            tmp.Message = message.Message;
            tmp.Exception = string.Empty;
            tmp.Level = "INFO";
            using (var log = client.GetTypedClient<Log>())
            {
                tmp.Id = log.GetNextSequence();
                using (var trans = client.CreateTransaction())
                {
                    trans.QueueCommand(x => x.Store(tmp));
                    trans.Commit();
                }
            }
        }

        public void InsertWarningMessage(DistALMessages.WarningMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity);
            var tmp = new Log();
            tmp.Date = message.Date;
            tmp.AppIdentity = appid;
            tmp.Module = message.ModuleName ?? string.Empty;
            tmp.Message = message.Message??string.Empty;
            tmp.Exception = message.Exception ?? string.Empty;
            tmp.Level = "WARNING";
            using (var log = client.GetTypedClient<Log>())
            {
                tmp.Id = log.GetNextSequence();
                using (var trans = client.CreateTransaction())
                {
                    trans.QueueCommand(x => x.Store(tmp));
                    trans.Commit();
                }
            }
        }


        public long CheckAppId(string appname)
        {
            long id=0;
            using (var apps = client.GetTypedClient<Application>())
            {
                var app = apps.GetAll().Where(x => x.AppName == appname.ToLower()).FirstOrDefault();
                if (app == null)
                {
                    var tmp = new Application();
                    tmp.Id = apps.GetNextSequence();
                    tmp.AppName = appname.ToLower();
                    using (var trans = client.CreateTransaction())
                    {
                        trans.QueueCommand(x => x.Store(tmp));
                        trans.Commit();
                    }
                    id = tmp.Id;
                }
                else
                {
                    id = app.Id;
                }
            }
            return id;
        }


        public Log[] GetLog(int PageNumber, int ItemsPerPage, out int TotalPages)
        {
            Log[] logs=null;
            TotalPages = 0;
            using (var logcons = client.GetTypedClient<Log>())
            {
                logs = client.GetAll<Log>().OrderByDescending(x=>x.Id).Skip((PageNumber - 1) * ItemsPerPage).Take(ItemsPerPage).ToArray();
                var numitems = client.GetAll<Log>().Count;
                TotalPages = (numitems / ItemsPerPage);
                if ((numitems % ItemsPerPage) > 0)
                {
                    ++TotalPages;
                }
            }
            return logs;
        }


        public Log[] GetLog()
        {
            Log[] logs = null;
            using (var logcons = client.GetTypedClient<Log>())
            {
                logs = client.GetAll<Log>().OrderByDescending(x => x.Id).ToArray();
            }
            return logs;
        }
    }
}
