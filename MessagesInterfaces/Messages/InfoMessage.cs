using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistALMessages
{
    [Serializable]
    public class InfoMessage : IMessage
    {
        public MessageTypes MessageType
        {
            get { return MessageTypes.Info; }
        }

        public string OriginIdentity
        {
            get;
            set;
        }
        public string ModuleName { get; set; }
        public string Message { get; set; }
    }
}
