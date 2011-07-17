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

namespace DistALServer
{
    public class AppLogServer
    {
        private Configuration conf;
        private Context context;
        private Socket receiver;
        private string ZmqUrl;
        private PollItem[] items = new PollItem[1];
        //IObservable<IEventSource<ZMQ.PollHandler>> msgReceived;
        private readonly Subject<ZMQPollerParams> msgReceived = new Subject<ZMQPollerParams>();
        public AppLogServer(Configuration config)
        {
            this.conf = config;
            ZmqUrl = string.Format("tcp://{0}:{1}",config.ServerIP.ToString(),config.Port.ToString());
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
            thr.Start(new StartParameters { context = this.context, pollitems = this.items }); 
        }

        private void StartSubscriber()
        {
            OnSignalReceived.Subscribe(x =>
            {
                DistALServer.DAL.DataAccess dal = new DAL.DataAccess();
                string identity = x.socket.Recv(Encoding.Unicode, 1000);
                byte[] encMsg;
                IMessage message;
                if (x.socket.RcvMore)
                {
                    encMsg = x.socket.Recv(1000);
                    message = (IMessage)Utils.Deserialize(encMsg);
                    switch (message.MessageType)
                    {
                        case MessageTypes.Hit:
                            dal.InsertHitMessage((HitMessage)message);
                            break;
                        case MessageTypes.Info:
                            dal.InsertInfoMessage((InfoMessage)message);
                            break;
                        case MessageTypes.Debug:
                            dal.InsertDebugMessage((DebugMessage)message);
                            break;
                        case MessageTypes.Error:
                            dal.InsertErrorMessage((ErrorMessage)message);
                            break;
                        case MessageTypes.Warning:
                            dal.InsertWarningMessage((WarningMessage)message);
                            break;
                        case MessageTypes.Fatal:
                            dal.InsertFatalMessage((FatalMessage)message);
                            break;

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

        public IObservable<ZMQPollerParams> OnSignalReceived
        {
            get { return msgReceived.AsObservable(); }
        }

        public void Stop()
        {
            context.Dispose();
            receiver.Dispose();            
        }
    }
}
