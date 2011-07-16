using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using DistALMessages;

namespace DistALServer
{
    public class AppLogServer
    {
        private Configuration conf;
        private Context context;
        private Socket receiver;
        private string ZmqUrl;
        private PollItem[] items = new PollItem[1];
        IObservable<IEventSource<ZMQ.PollHandler>> msgReceived;
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
            Thread thr = new Thread(StartReceiver);
            thr.Start(new StartParameters { context = this.context, pollitems = this.items }); 
        }

        private void StartReceiver(object parameters)
        {
            StartParameters pollerParameters = (StartParameters)parameters;
            while (true)
            {
                pollerParameters.context.Poll(pollerParameters.pollitems, -1);
                System.Threading.Thread.Sleep(200);
            }
        }
        private void MessageReceiver_PollInHandler(Socket socket, IOMultiPlex revents)
        {
            //byte[] msgData = receiver.Recv(10000);
            DistALServer.DAL.DataAccess dal = new DAL.DataAccess();
            string identity=receiver.Recv(Encoding.Unicode, 1000);
            byte[] encMsg;
            IMessage message;
            if (receiver.RcvMore)
            {
                encMsg = receiver.Recv(1000);
                message=(IMessage)Utils.Deserialize(encMsg);
                switch (message.MessageType)
                {
                    case MessageTypes.Hit:
                        dal.InsertHitMessage((HitMessage)message);
                        break;
                    case MessageTypes.Info:
                        dal.InsertInfoMessage((InfoMessage)message);
                        break;
                }
            }
            Console.WriteLine("Message Received of "+identity+":");
            
        }
        public void Stop()
        {
            context.Dispose();
            receiver.Dispose();            
        }
    }
}
