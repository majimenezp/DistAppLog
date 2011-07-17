using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;

namespace DistALServer
{
    public class ZMQPollerParams
    {
        public Socket socket {get;set;}
        public IOMultiPlex revents {get;set;}
    }
}
