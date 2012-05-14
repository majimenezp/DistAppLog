// -----------------------------------------------------------------------
// <copyright file="SocketFactory.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DistALClient
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ZMQ;
    using System.Threading;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class SocketFactory
    {
        private static int poolMaxSize = 10;
        private static readonly Queue<Socket> socketPool = new Queue<Socket>(poolMaxSize);
        private static readonly object lockObj=new object();

        private int socketCount = 0;
        private static SocketFactory factoryInstance = null;
        private Context context;
        private Semaphore sync;
        private SocketFactory()
        {
            context = new Context();
            sync = new Semaphore(poolMaxSize, poolMaxSize);
        }

        static SocketFactory()
        {
            factoryInstance = new SocketFactory();
        }

        public static SocketFactory Instance
        {
            get
            {
                if (factoryInstance != null)
                {
                    return factoryInstance;
                }
                return null;
            }
        }

        public void CreateSockets(string serverUrl,string identity)
        {
            Socket tmpSocket;
            socketCount = 0;
            socketPool.Clear();
            for (int i = 0; i < poolMaxSize; i++)
            {
                tmpSocket = context.Socket(SocketType.PUB);
                tmpSocket.Identity=Encoding.Unicode.GetBytes(identity+"-"+i.ToString());
                tmpSocket.Connect(serverUrl);
                lock (lockObj)
                {
                    socketPool.Enqueue(tmpSocket);                    
                    socketCount++;
                }
            }
        }

        public Socket Acquire()
        {
            sync.WaitOne();
            lock (socketPool)
            {
                return socketPool.Dequeue();
            }
        }
        public void Release(Socket socket)
        {
            lock (socketPool)
            {
                socketPool.Enqueue(socket);
            }
            sync.Release();
        }
    }
}
