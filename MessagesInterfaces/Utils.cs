using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ProtoBuf;
using ProtoBuf.Meta;

namespace DistALMessages
{
    public class Utils
    {
        public static byte[] Serialize(object instance)
        {
            MemoryStream mem = new MemoryStream();
            ProtoBuf.Serializer.Serialize(mem, instance);
            return mem.ToArray();
        }

        public static T Deserialize<T>(byte[] bytearray)
        {
            T resultado = default(T);
            MemoryStream mem = new MemoryStream(bytearray);
            resultado = ProtoBuf.Serializer.Deserialize<T>(mem);
            return resultado;
        }

        public static void ConfigureDeserialization()
        {

            //RuntimeTypeModel.Default.Add(typeof(IMessage), true)
            //    .AddSubType(50, typeof(InfoMessage))
            //    .AddSubType(51, typeof(DebugMessage))
            //    .AddSubType(52, typeof(HitMessage))
            //    .AddSubType(53, typeof(WarningMessage))
            //    .AddSubType(54, typeof(ErrorMessage))
            //    .AddSubType(55, typeof(FatalErrorMessage));
            //    .AddSubType(50,typeof(InfoMessage))
            //    .AddSubType(51,typeof(HitMessage));
        }
    }
}
