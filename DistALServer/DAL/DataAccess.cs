using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistALMessages;

namespace DistALServer.DAL
{
    public class DataAccess
    {
        List<dynamic> ApplicationsIds;
        public DataAccess()
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
                Level="Info",
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
                Level = "Hit",
                Message = "Hit by user:" +message.User+"|Message:"+message.Message,
                Exception = string.Empty
            });
        }

        private long CheckAppId(string appname)
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
    }
}
