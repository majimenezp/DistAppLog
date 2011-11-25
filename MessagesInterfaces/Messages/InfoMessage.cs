using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace DistALMessages
{
    [Serializable]
    [ProtoContract]
    public class InfoMessage : IMessage
    {
        public MessageTypes MessageType
        {
            get { return MessageTypes.Info; }
        }

         [ProtoMember(8)]
        public string OriginIdentity
        {
            get;
            set;
        }
         [ProtoMember(9)]
        public string ModuleName { get; set; }

         [ProtoMember(10)]
        public string Message { get; set; }
    }
}
