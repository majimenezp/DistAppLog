using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistALClient;

namespace DistALTestHostClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration config = new Configuration();
            config.Port = 5560;
            config.ServerIP = config.StringToIP("192.168.1.72");
            if (args.Length > 0)
            {
                config.Identity = args[0];
            }
            else
            {
                config.Identity = "Client1";
            }
            
            AppLogClient.Instance.Init(config);
            for (int i = 0; i < 100; i++)
            {
                AppLogClient.Instance.SendInfoMessage("TestClient", "Test " + i.ToString());
                System.Threading.Thread.Sleep(1000);
            }
            Console.WriteLine("Press a key to stop...");
            Console.ReadKey();
        }
    }
}
