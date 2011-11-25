using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using DistALMessages;
using System.Reactive.Subjects;
using Ninject;
using ProtoBuf.Meta;
using Kayak;
using Gate;
using Gate.Kayak;
using DistALServer.NancyKayak;
using System.Net;

namespace DistALServer
{
    public class AppLogServer
    {
        private Configuration conf;
        private Context context;
        private Socket receiver;
        private string ZmqUrl;
        private static DistALServer.DAL.IDataAccess dal;
        private PollItem[] items = new PollItem[1];
        private int webport;
        //IObservable<IEventSource<ZMQ.PollHandler>> msgReceived;
        private readonly Subject<ZMQPollerParams> msgReceived = new Subject<ZMQPollerParams>();
        private static IScheduler scheduler;
        private static Thread hiloServer;
        public AppLogServer()
        {
            DistAppLogConfigurationSection config =
                       (DistAppLogConfigurationSection)System.Configuration.ConfigurationManager.GetSection("DistAppLogSection/Server");
            ZmqUrl = string.Format("tcp://*:{0}", config.Communication.TcpPort.ToString());
            IKernel kernel = NinjectFactory.GetNinjectKernel();
            var tipo = Type.GetType(config.DataBase.DatabaseProvider);
            dal = (DistALServer.DAL.IDataAccess)kernel.Get(tipo);
            Utils.ConfigureDeserialization();
            webport = config.Communication.WebServerPort;
            //dal= 
        }

        public void Start()
        {
            context = new Context(1);
            receiver = context.Socket(SocketType.ROUTER);
            receiver.Bind(ZmqUrl);
            Console.WriteLine("The server was started");
            items[0] = receiver.CreatePollItem(IOMultiPlex.POLLIN);
            items[0].PollInHandler += new PollHandler(MessageReceiver_PollInHandler);

            StartSubscriber();
            Thread thr = new Thread(StartReceiver);
            hiloServer = new Thread(new ParameterizedThreadStart(InicioWebserver));
            thr.Start(new StartParameters { context = this.context, pollitems = this.items });
            hiloServer.Start(webport);
        }

        public static void InicioWebserver(object parametro)
        {
            int puerto = Convert.ToInt32(parametro);
            var schedulerDelegate = new SchedulerDelegate();
            scheduler = KayakScheduler.Factory.Create(schedulerDelegate);
            var appDelegate = AppBuilder.BuildConfiguration(Startup.Configuration);
            var endpoint = new IPEndPoint(IPAddress.Any, puerto);
            using (KayakServer.Factory.CreateGate(appDelegate, scheduler, null).Listen(endpoint))
            {
                scheduler.Start();
            }
        }

        public IObservable<ZMQPollerParams> OnSignalReceived
        {
            get { return msgReceived.AsObservable(); }
        }

        public void Stop()
        {
            items[0].PollInHandler -= new PollHandler(MessageReceiver_PollInHandler);
            context.Dispose();
            receiver.Dispose();
            if (scheduler != null)
            {
                scheduler.Stop();
            }
            if (hiloServer != null)
            {
                hiloServer.Join();
            }
        }

        public static DistALServer.DAL.IDataAccess Dal
        {
            get { return dal; }
        }

        private AppLogServer(Configuration config)
        {
            DistAppLogConfigurationSection config1 =
            (DistAppLogConfigurationSection)System.Configuration.ConfigurationManager.GetSection("DistAppLogSection/Server");

            this.conf = config;
            ZmqUrl = string.Format("tcp://{0}:{1}", config.ServerIP.ToString(), config.Port.ToString());
        }

        private void StartSubscriber()
        {
            OnSignalReceived.Subscribe(x =>
            {
                string identity = x.socket.Recv(Encoding.Unicode, 1000);
                byte[] encMsg;
                MessageWrapper message;
                if (x.socket.RcvMore)
                {
                    encMsg = x.socket.Recv(1000);
                    if (encMsg.Length > 0)
                    {
                        message = Utils.Deserialize<MessageWrapper>(encMsg);
                        switch (message.Message.MessageType)
                        {
                            case MessageTypes.Hit:
                                dal.InsertHitMessage((HitMessage)message.Message);
                                break;
                            case MessageTypes.Info:
                                dal.InsertInfoMessage((InfoMessage)message.Message);
                                break;
                            case MessageTypes.Debug:
                                dal.InsertDebugMessage((DebugMessage)message.Message);
                                break;
                            case MessageTypes.Error:
                                dal.InsertErrorMessage((ErrorMessage)message.Message);
                                break;
                            case MessageTypes.Warning:
                                dal.InsertWarningMessage((WarningMessage)message.Message);
                                break;
                            case MessageTypes.Fatal:
                                dal.InsertFatalMessage((FatalErrorMessage)message.Message);
                                break;

                        }
                    }
                }
                Console.WriteLine("Message Received of " + identity + ":");
            });
        }

        private void StartReceiver(object parameters)
        {
            StartParameters pollerParameters = (StartParameters)parameters;
            while (true)
            {
                pollerParameters.context.Poll(pollerParameters.pollitems, -1);
            }
        }

        private void MessageReceiver_PollInHandler(Socket socket, IOMultiPlex revents)
        {
            msgReceived.OnNext(new ZMQPollerParams { socket = socket, revents = revents });
        }


    }
}
