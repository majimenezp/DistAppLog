using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace DistALMessages
{
    [Serializable]
    [ProtoContract]
    public class DebugMessage:IMessage
    {
        public MessageTypes MessageType
        {
            get { return MessageTypes.Debug; }
        }

        [ProtoMember(8)]
        public string OriginIdentity
        {
            get;
            set;
        }
        [ProtoMember(9)]
        public DateTime Date { get; set; }

        [ProtoMember(10)]
        public string ModuleName { get; set; }

        [ProtoMember(11)]
        public string Message { get; set; }

        [ProtoMember(12)]
        public string Stacktrace { get; set; }

    }
}
