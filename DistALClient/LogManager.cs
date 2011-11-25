using System;
using System.Collections.Generic;
using System.Text;

namespace DistALClient
{
    public class LogManager
    {
        public static ILog GetLogger(string Name)
        {
            return new LogWrapper(Name);
        }
    }
}
