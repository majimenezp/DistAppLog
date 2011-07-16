using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;
using DistALMessages;

namespace DistALClient
{
    public class AppLogClient:IDisposable
    {
        private string ZmqUrl;
        private Configuration conf;
        private static AppLogClient instance;
        private Context context;
        private Socket sender;
        private AppLogClient()
        {
        }
        public static AppLogClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppLogClient();
                }
                return instance;
            }
        }
        public void Init(Configuration config)
        {
            conf = config;
            ZmqUrl = string.Format("tcp://{0}:{1}", config.ServerIP.ToString(), config.Port.ToString());
            context = new Context(1);
            sender = context.Socket(SocketType.DEALER);
            sender.StringToIdentity(config.Identity, Encoding.Unicode);
            sender.Connect(ZmqUrl);
        }

        public void SendHitMessage(DateTime time, string ModuleName, string user, string Message)
        {
            HitMessage message = new HitMessage();
            message.DateofHit = time;
            message.ModuleName = ModuleName;
            message.User = user;
            message.Message = Message;
            SendHitMessage(message);
        }
        public void SendHitMessage(HitMessage message)
        {
            message.OriginIdentity = conf.Identity;
            byte[] encMessage=Utils.Serializer(message);
            sender.Send(encMessage);
        }

        public void SendInfoMessage(string ModuleName,string Message)
        {
            InfoMessage message = new InfoMessage();
            message.Message = Message;
            message.ModuleName = ModuleName;
            SendInfoMessage(message);
        }

        private void SendInfoMessage(InfoMessage message)
        {
            message.OriginIdentity = conf.Identity;
            byte[] encMessage = Utils.Serializer(message);
            sender.Send(encMessage);
        }


        public void Dispose()
        {
            
        }
    }
}
