using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
namespace DistALMessages
{
    [ProtoContract]
    [ProtoInclude(51, typeof(InfoMessage))]
    [ProtoInclude(52, typeof(DebugMessage))]
    [ProtoInclude(53, typeof(HitMessage))]
    [ProtoInclude(54, typeof(WarningMessage))]
    [ProtoInclude(55, typeof(FatalErrorMessage))]
    [ProtoInclude(56, typeof(ErrorMessage))]    
    public interface IMessage
    {
        MessageTypes MessageType { get; }
        [ProtoMember(7)]
        string OriginIdentity { get; set; }
    }
}
