using System;
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
            AppLogServer srv = new AppLogServer();
            srv.Start();
            Console.WriteLine("Press a key to stop...");
            Console.ReadKey();
            srv.Stop();
            
        }
    }
}
