using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DistALMessages
{
    public class Utils
    {
        public static byte[] Serializer(object instance)
        {
            MemoryStream mem=new MemoryStream();
            BinaryFormatter serl = new BinaryFormatter();
            serl.Serialize(mem, instance);
            return mem.ToArray();
        }
        public static object Deserialize(byte[] bytearray)
        {
            BinaryFormatter dserl = new BinaryFormatter();
            MemoryStream mem=new MemoryStream(bytearray);
            return dserl.Deserialize(mem);
        }
    }
}
