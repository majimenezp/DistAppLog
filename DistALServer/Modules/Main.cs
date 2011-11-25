using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
namespace DistALServer.Modules
{
    public class Main:NancyModule
    {
        public Main()
        {
            Get["/"] = x =>
                {
                    var logs= AppLogServer.Dal.GetLog();
                    var model=new { Title="Logs",Logs=logs};
                    return View["Index", model];
                };
        }
    }
}
