using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistALMessages
{
    [Serializable]
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
        public string OriginIdentity
        {
            get;
            set;
        }
        public DateTime DateofHit { get; set; }
        public string ModuleName { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
    }
}
