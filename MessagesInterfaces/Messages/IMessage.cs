using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistALMessages
{
    
    public interface IMessage
    {
        MessageTypes MessageType { get; }
        string OriginIdentity { get; set; }
    }
}
