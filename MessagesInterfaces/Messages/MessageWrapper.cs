using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using DistALMessages;

namespace DistALMessages
{
    [ProtoContract]
    public class MessageWrapper
    {
        [ProtoMember(1)]
        public IMessage Message { get; set; }
    }
}
