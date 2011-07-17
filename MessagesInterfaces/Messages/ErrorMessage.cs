using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistALMessages
{
    [Serializable]
    public class ErrorMessage : IMessage
    {
        public DateTime Date { get; set; }

        public string ModuleName { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public virtual MessageTypes MessageType
        {
            get { return MessageTypes.Error; }
        }

        public string OriginIdentity
        {
            get;
            set;
        }
    }
}
