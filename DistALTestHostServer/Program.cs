﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistALServer;

namespace DistALTestHostServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration config=new Configuration();
            config.Port=5560;
            config.ServerIP=config.StringToIP("192.168.1.72");
            AppLogServer srv = new AppLogServer(config);
            srv.Start();
            Console.WriteLine("Press a key to stop...");
            Console.ReadKey();
            srv.Stop();
            
        }
    }
}
