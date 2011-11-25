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
    public static class Startup
    {
        public static void Configuration(IAppBuilder builder)
        {
            builder
               .RescheduleCallbacks()
               .Use(del => (env, result, fault) =>
               {
                   del(env, (status, headers, body) =>
                   {
                       if (!headers.ContainsKey("Content-Type") || string.IsNullOrEmpty(headers["Content-Type"]))
                           headers["Content-Type"] = "text/html";
                       result(status, headers, body);
                   }, fault);
               })
               .RunNancy();
        }
    }
}
