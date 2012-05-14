using System;
using System.Collections.Generic;
using System.Text;
using ZMQ;
using DistALMessages;
using System.Threading;

namespace DistALClient
{
    public class AppLogClient:IDisposable
    {
        private string ZmqUrl;
        private static AppLogClient instance=new AppLogClient();
        private static string identity;

        static AppLogClient()
        {

        }
        private AppLogClient()
        {
            Console.WriteLine("Instance created");
            DistAppLogConfigurationSection config =
                       (DistAppLogConfigurationSection)System.Configuration.ConfigurationManager.GetSection("DistAppLogSection/Client");
            ZmqUrl = string.Format("tcp://{0}:{1}", config.Communication.Server.ToString(), config.Communication.TcpPort.ToString());
            identity = config.Communication.Identity;
            SocketFactory.Instance.CreateSockets(ZmqUrl,identity);
            
        }
        public static AppLogClient Instance
        {
            get
            {
                return instance;
            }
        }

        public void SendHitMessage(DateTime time, string ModuleName, string user, string Message)
        {
            HitMessage message = new HitMessage();
            message.DateofHit = time;
            message.ModuleName = ModuleName;
            message.User = user;
            message.Message = Message;
            message.OriginIdentity = identity;
            SendHitMessage(message);
        }
        public void SendHitMessage(HitMessage message)
        {            
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadSend<HitMessage>), message);
        }

        public void SendInfoMessage(string ModuleName,string Message)
        {
            InfoMessage message = new InfoMessage();
            message.Message = Message;
            message.ModuleName = ModuleName;
            message.OriginIdentity = identity;
            SendInfoMessage(message);
        }

        public void SendInfoMessage(InfoMessage message)
        {            
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadSend<InfoMessage>), message);
        }

        public void SendErrorMessage(string ModuleName, string Message)
        {
            ErrorMessage message = new ErrorMessage();
            message.Date = DateTime.Now;
            message.Message = Message;
            message.ModuleName = ModuleName;
            message.Exception = string.Empty;
            message.OriginIdentity = identity;
            SendErrorMessage(message);
        }

        public void SendErrorMessage(string ModuleName,string Message,System.Exception exception)
        {
            ErrorMessage message = new ErrorMessage();
            message.Date = DateTime.Now;
            message.Message = Message;
            message.ModuleName = ModuleName;
            message.Exception = exception.ToString();
            message.OriginIdentity = identity;
            SendErrorMessage(message);
        }
        public void SendErrorMessage(ErrorMessage message)
        {            
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadSend<ErrorMessage>), message);          
        }

        public void SendWarningMessage(string ModuleName, string Message)
        {
            WarningMessage message = new WarningMessage();
            message.Date = DateTime.Now;
            message.Message = Message;
            message.ModuleName = ModuleName;
            message.Exception = string.Empty;
            message.OriginIdentity = identity;
            SendWarningMessage(message);
        }

        public void SendWarningMessage(string ModuleName, string Message, System.Exception exception)
        {
            WarningMessage message = new WarningMessage();
            message.Date = DateTime.Now;
            message.Message = Message;
            message.ModuleName = ModuleName;
            message.Exception = exception.ToString();
            message.OriginIdentity = identity;
            SendWarningMessage(message);
        }
        public void SendWarningMessage(WarningMessage message)
        {           
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadSend<WarningMessage>), message);
        }

        public void SendFatalMessage(string ModuleName, string Message, System.Exception exception)
        {
            FatalErrorMessage message = new FatalErrorMessage();
            message.Date = DateTime.Now;
            message.Message = Message;
            message.ModuleName = ModuleName;
            message.Exception = exception.ToString();
            message.OriginIdentity = identity;
            SendFatalMessage(message);
        }

        public void SendFatalMessage(string ModuleName, string Message)
        {
            FatalErrorMessage message = new FatalErrorMessage();
            message.Date = DateTime.Now;
            message.Message = Message;
            message.ModuleName = ModuleName;
            message.Exception = string.Empty;
            message.OriginIdentity = identity;
            SendFatalMessage(message);
        }

        public void SendFatalMessage(FatalErrorMessage message)
        {           
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadSend<FatalErrorMessage>), message);
        }

        public void SendDebugMessage(string ModuleName, string Message)
        {
            List<string> frames1 = new List<string>();
            DebugMessage message = new DebugMessage();
            message.Message = Message;
            message.ModuleName = ModuleName;
            message.Date = DateTime.Now;
            System.Diagnostics.StackTrace st=new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame[] frames=st.GetFrames();
            foreach (System.Diagnostics.StackFrame frame in frames)
            {
                frames1.Add(frame.GetMethod().Name);
            }
            // used with 3.5 framework
            //string.Join("->",(from fr in frames select fr.GetMethod().Name).ToArray());
            message.Stacktrace = string.Join("->",frames1.ToArray());
            message.OriginIdentity = identity;
            SendDebugMessage(message);
        }
        public void SendDebugMessage(DebugMessage message)
        {            
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadSend<DebugMessage>), message);
        }

        static void ThreadSend<T>(object sendinfo) where T :IMessage
        {
            T message=(T)sendinfo;
            MessageWrapper wrap = new MessageWrapper();
            wrap.Message = message;
            byte[] encMessage = Utils.Serialize(wrap);
            
            try
            {
                Socket sender = SocketFactory.Instance.Acquire();
                sender.Send(encMessage);
                SocketFactory.Instance.Release(sender);
            }
            catch (System.Exception ex1)
            {
            }
        }        
        public void Dispose()
        {
            
        }
    }
}
