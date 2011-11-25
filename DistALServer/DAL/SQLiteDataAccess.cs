using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistALMessages;
using DistALServer.DAL.SQLite;
namespace DistALServer.DAL
{
    public class SQLiteDataAccess : DistALServer.DAL.IDataAccess
    {
        List<dynamic> ApplicationsIds;
        public SQLiteDataAccess()
        {
            var tb=new Applications();
            ApplicationsIds = tb.All().ToList();
        }
        public void InsertInfoMessage(InfoMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity.ToLower());
            var tb = new Log();
            var newId = tb.Insert(new {
                Date=DateTime.Now,
                AppIdentity=appid,
                Module=message.ModuleName,
                Level="INFO",
                Message=message.Message,
                Exception=string.Empty
            });
        }

        public void InsertHitMessage(HitMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity.ToLower());
            var tb = new Log();
            var newId = tb.Insert(new
            {
                Date = message.DateofHit,
                AppIdentity = appid,
                Module = message.ModuleName,
                Level = "HIT",
                Message = "Hit by user:" +message.User+"|Message:"+message.Message,
                Exception = string.Empty
            });
        }

        public long CheckAppId(string appname)
        {            
            var reg = ApplicationsIds.FirstOrDefault(x => x.AppName == appname);
            if (reg != null)
            {
                return reg.Id;
            }
            else
            {
                // create new appid
                var apps = new Applications();
                long newId = (long)apps.Insert(new { AppName=appname});
                ApplicationsIds.Add(new { Id = newId, AppName = appname });
                return newId;
            }
        }

        public void InsertDebugMessage(DebugMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity.ToLower());
            var tb = new Log();
            var newId = tb.Insert(new
            {
                Date = message.Date,
                AppIdentity = appid,
                Module = message.ModuleName,
                Level = "DEBUG",
                Message = message.Message,
                Exception = message.Stacktrace
            });
        }

        public void InsertErrorMessage(ErrorMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity.ToLower());
            var tb = new Log();
            var newId = tb.Insert(new
            {
                Date = message.Date,
                AppIdentity = appid,
                Module = message.ModuleName,
                Level = "ERROR",
                Message = message.Message,
                Exception = message.Exception.ToString()
            });
        }

        public void InsertWarningMessage(WarningMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity.ToLower());
            var tb = new Log();
            var newId = tb.Insert(new
            {
                Date = message.Date,
                AppIdentity = appid,
                Module = message.ModuleName,
                Level = "WARNING",
                Message = message.Message,
                Exception = message.Exception.ToString()
            });
        }

        public void InsertFatalMessage(FatalErrorMessage message)
        {
            long appid = CheckAppId(message.OriginIdentity.ToLower());
            var tb = new Log();
            var newId = tb.Insert(new
            {
                Date = message.Date,
                AppIdentity = appid,
                Module = message.ModuleName,
                Level = "FATAL",
                Message = message.Message,
                Exception = message.Exception.ToString()
            });
        }


        public Entities.Log[] GetLog(int PageNumber, int ItemsPerPage, out int TotalPages)
        {
            throw new NotImplementedException();
        }


        public Entities.Log[] GetLog()
        {
            throw new NotImplementedException();
        }
    }
}
