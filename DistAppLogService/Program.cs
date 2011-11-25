using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace DistAppLogService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].Equals("/i", StringComparison.InvariantCultureIgnoreCase))
                {
                    System.Configuration.Install.ManagedInstallerClass.InstallHelper(new string[] {
                        System.Reflection.Assembly.GetExecutingAssembly().Location});
                }
                else if (args[0].Equals("/u", StringComparison.InvariantCultureIgnoreCase))
                {
                    System.Configuration.Install.ManagedInstallerClass.InstallHelper(new string[] {"/u",
                        System.Reflection.Assembly.GetExecutingAssembly().Location});
                }
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
			    { 
				    new DistAppLogService() 
			    };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
