using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;


namespace DistALMessages
{
    [Serializable]
    [ProtoContract]
    public class HitMessage:IMessage
    {
        public HitMessage()
        {
            DateofHit = DateTime.Now;
        }
        public MessageTypes MessageType
        {
            get
            {
                return MessageTypes.Hit;
            }            
        }
        [ProtoMember(8)]
        public string OriginIdentity
        {
            get;
            set;
        }
        [ProtoMember(9)]
        public DateTime DateofHit { get; set; }
        [ProtoMember(10)]
        public string ModuleName { get; set; }
        [ProtoMember(11)]
        public string User { get; set; }
        [ProtoMember(12)]
        public string Message { get; set; }
    }
}
