using System;
using System.Collections.Generic;
using System.Text;
using DistALClient;
using System.Threading;

namespace DistALTestHostClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int i ;
            Configuration config = new Configuration();
            config.Port = 5560;
            config.ServerIP = config.StringToIP("172.21.138.75");
            if (args.Length > 0)
            {
                config.Identity = args[0];
            }
            else
            {
                config.Identity = "Client1";
            }
            Random rand = new Random(544242424);
           
            
            AppLogClient.Instance.Init(config);
            for (i= 0; i < 10; i++)
            {
                AppLogClient.Instance.SendInfoMessage("TestClient", "Test " + i.ToString());
                Thread.Sleep( rand.Next(1, 500));
            }
            for (i = 0; i < 10; i++)
            {
                AppLogClient.Instance.SendHitMessage(DateTime.Now, "TestHit", "Me", "Test " + i.ToString());
                Thread.Sleep(rand.Next(1, 500));
            }
            for (i = 0; i < 10; i++)
            {
                AppLogClient.Instance.SendErrorMessage("TestError", "Error test" + i.ToString(), new Exception() { Source = "Test sender" });
                Thread.Sleep(rand.Next(1, 500));
            }
            for (i = 0; i < 10; i++)
            {
                AppLogClient.Instance.SendDebugMessage("TestDebug", "Test debug" + i.ToString());
                Thread.Sleep(rand.Next(1, 500));
            }
            for (i = 0; i < 10; i++)
            {
                AppLogClient.Instance.SendWarningMessage("TestWarning","Test warn" + i.ToString(),new Exception() { Source = "Test sender" });
                Thread.Sleep(rand.Next(1, 500));
            }
            for (i = 0; i < 10; i++)
            {
                AppLogClient.Instance.SendFatalMessage("TestFatal", "Test fatal" + i.ToString(), new Exception() { Source = "Test sender" });
                Thread.Sleep(rand.Next(1, 500));
            }
            Console.WriteLine("Press a key to stop...");
            Console.ReadKey();
        }
    }
}
