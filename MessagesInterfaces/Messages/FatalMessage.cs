using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
namespace DistALMessages
{
    [Serializable]
    [ProtoContract]
    public class FatalErrorMessage : ErrorMessage
    {
        public override MessageTypes MessageType
        {
            get
            {
                return MessageTypes.Fatal;
            }
        }
    }
}
