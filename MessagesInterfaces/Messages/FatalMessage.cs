using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistALMessages
{
    [Serializable]
    public class FatalMessage : ErrorMessage
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
