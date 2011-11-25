using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using DistALServer;

namespace DistAppLogService
{
    public partial class DistAppLogService : ServiceBase
    {
        AppLogServer srv;
        public DistAppLogService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            srv = new AppLogServer();
            srv.Start();
        }

        protected override void OnStop()
        {
            srv.Stop();
        }
    }
}
