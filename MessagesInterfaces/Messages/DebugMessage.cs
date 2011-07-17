using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistALMessages
{
    [Serializable]
    public class DebugMessage:IMessage
    {
        public MessageTypes MessageType
        {
            get { return MessageTypes.Debug; }
        }

        public string OriginIdentity
        {
            get;
            set;
        }
        public DateTime Date { get; set; }

        public string ModuleName { get; set; }
        public string Message { get; set; }
        public string Stacktrace { get; set; }

    }
}
