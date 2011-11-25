using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kayak;
using Nancy.Hosting.Owin;
using Gate;
using Gate.Kayak;

namespace DistALServer.NancyKayak
{
    public class SchedulerDelegate : ISchedulerDelegate
    {
        public void OnException(IScheduler scheduler, Exception e)
        {
            Console.WriteLine("Exception on scheduler");
            Console.Out.WriteStackTrace(e);
        }

        public void OnStop(IScheduler scheduler)
        {
            Console.WriteLine("Scheduler is stopping.");
        }
    }
}
