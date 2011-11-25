using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace DistALMessages
{
    [Serializable]
    [ProtoContract]
    public class ErrorMessage : IMessage
    {
        [ProtoMember(8)]
        public DateTime Date { get; set; }

        [ProtoMember(9)]
        public string ModuleName { get; set; }

        [ProtoMember(10)]
        public string Message { get; set; }

        [ProtoMember(11)]
        public string Exception { get; set; }

        public virtual MessageTypes MessageType
        {
            get { return MessageTypes.Error; }
        }

        [ProtoMember(12)]
        public string OriginIdentity
        {
            get;
            set;
        }
    }
}
